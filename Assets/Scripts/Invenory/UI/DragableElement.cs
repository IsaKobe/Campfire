using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Invenory.UI
{
    public class DragableElement : PointerManipulator
    {
        
        Vector2 startPanel;
        Vector2 startWorld;
        WindowsToggles invenoryView;
        bool dragging;

        public DragableElement(VisualElement tar, WindowsToggles _invenory)
        {
            target = tar;
            invenoryView = _invenory;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(InitDrag);
            target.RegisterCallback<PointerMoveEvent>(Drag);
            target.RegisterCallback<PointerCaptureOutEvent>(CaptureOut);
            target.RegisterCallback<PointerUpEvent>(EndDrag);
        }

        private void EndDrag(PointerUpEvent evt)
        {
            target.ReleasePointer(evt.pointerId);
            evt.StopPropagation();
        }

        private void Drag(PointerMoveEvent evt)
        {
            if (dragging == false)
                return;

            Vector2 vec = evt.position;

            vec = vec - startPanel;
            vec = startWorld + vec;
            vec = target.parent.WorldToLocal(vec);
            target.style.left = vec.x;
            target.style.top = vec.y;

            evt.StopPropagation();
        }

        private void InitDrag(PointerDownEvent evt)
        {
            if (evt.button != 0)
                return;
            target.style.position = Position.Absolute;
            target.style.left = target.localBound.x;
            target.style.top = target.localBound.y;

            dragging = true;
            startPanel = evt.position;
            startWorld = target.worldBound.position;
            target.BringToFront();
            target.CapturePointer(evt.pointerId);

            evt.StopPropagation();
        }
        private void CaptureOut(PointerCaptureOutEvent evt)
        {
            dragging = false;

            invenoryView.ChangeNear(target);

            target.style.position = Position.Relative;
            target.style.left = StyleKeyword.None;
            target.style.top = StyleKeyword.None;

            evt.StopPropagation();
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(InitDrag);
            target.UnregisterCallback<PointerMoveEvent>(Drag);
            target.UnregisterCallback<PointerCaptureOutEvent>(CaptureOut);
            target.UnregisterCallback<PointerUpEvent>(EndDrag);
        }

        

    }
}
