using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetcodePlus;

namespace SurvivalEngine
{
    [RequireComponent(typeof(Selectable))]
    public class Door : SNetworkBehaviour
    {
        private Selectable select;
        private Animator animator;
        private Collider collide;

        private SNetworkActions actions;
        private bool opened = false;

        void Start()
        {
            select = GetComponent<Selectable>();
            animator = GetComponentInChildren<Animator>();
            collide = GetComponentInChildren<Collider>();
            select.onUse += OnUse;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            actions = new SNetworkActions(this);
            actions.Register("open", DoOpen);
            actions.Register("close", DoClose);
        }

        protected override void OnDespawn()
        {
            base.OnDespawn();
            actions.Clear();
        }

        private void OnUse(PlayerCharacter character)
        {
            Toggle();
        }

        public void Toggle()
        {
            if (!opened)
                Open();
            else
                Close();
        }

        public void Open()
        {
            actions?.Trigger("open"); // DoOpen()
        }

        public void Close()
        {
            actions?.Trigger("close"); // DoClose()
        }

        private void DoOpen()
        {
            opened = true;
            Refresh();
        }

        private void DoClose()
        {
            opened = false;
            Refresh();
        }

        private void Refresh()
        {
            if (collide != null)
                collide.isTrigger = opened;

            if (animator != null)
                animator.SetBool("Open", opened);
        }
    }

}
