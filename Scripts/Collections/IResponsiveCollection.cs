using System;

namespace MECS.Collections
{
    //* Interface used to set base events on responsive collections
    public interface IResponsiveCollection<T>
    {
        //Events used on responsive collections
        public event EventHandler<T> ElementAddedEvent,
            ElementRemovedEvent,
            FirstElementAddedEvent,
            LastElementRemovedEvent;
    }
}