using System;

namespace Unity.VisualScripting
{
    /// <summary>
    /// Called each frame where a collider on another object is touching this object's collider.
    /// </summary>
    public sealed class OnCollisionStay2D : CollisionEvent2DUnit
    {
        public override Type MessageListenerType => typeof(UnityOnCollisionStay2DMessageListener);
        protected override string hookName => EventHooks.OnCollisionStay2D;
    }
}
