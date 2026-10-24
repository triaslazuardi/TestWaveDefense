using Nitzz.Game.Core;
using Nitzz.Game.Data;
using Nitzz.Game.Structures;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nitzz.Game.Build
{
    public class BuildController : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] StructureData[] available;
        [SerializeField] SpriteRenderer ghost;

        StructureData selected;

        public void Select(int index)
        {
            if (index < 0 || index >= available.Length) return;
            selected = available[index];
            ghost.sprite = selected.prefab.GetComponent<SpriteRenderer>().sprite;
        }

        void Deselect()
        {
            selected = null;
            ghost.enabled = false;
        }

        void Update()
        {
            if (selected == null || !GameManager.Instance.CanBuild)
            {
                ghost.enabled = false;
                return;
            }

            if (Input.GetMouseButtonDown(1)) { Deselect(); return; }

            BuildSlot slot = GetSlotUnderMouse();
            if (slot == null)
            {
                ghost.enabled = false;
                return;
            }

            bool valid = slot.IsFree && ResourceManager.Instance.CanAfford(selected.cost);

            ghost.enabled = true;
            ghost.transform.position = slot.transform.position;
            ghost.color = valid ? new Color(0.4f, 1f, 0.4f, 0.6f)
                                : new Color(1f, 0.3f, 0.3f, 0.6f);

            if (Input.GetMouseButtonDown(0) && valid && !EventSystem.current.IsPointerOverGameObject())
                Place(slot);
        }

        BuildSlot GetSlotUnderMouse()
        {
            Vector2 world = cam.ScreenToWorldPoint(Input.mousePosition);
            foreach (var hit in Physics2D.OverlapPointAll(world))
            {
                var slot = hit.GetComponent<BuildSlot>();
                if (slot != null) return slot;
            }
            return null;
        }

        void Place(BuildSlot slot)
        {
            if (!ResourceManager.Instance.TrySpend(selected.cost)) return;

            var go = Instantiate(selected.prefab, slot.transform.position, Quaternion.identity);
            var structure = go.GetComponent<Structure>();
            structure.Initialize(selected);
            slot.Occupy(structure);
        }
    }
}