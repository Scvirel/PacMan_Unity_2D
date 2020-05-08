namespace Game.Model
{
    public abstract partial class ModelBase
    {
        protected interface IContext
        {
            IField Field { get; }
        }

        protected interface IContextWritable : IContext
        {
            new IField Field { get; }
            ICharactersContainer CharactardsContainer { get; set; }

            IEventManagerInternal EventManager { get; }
        }

        // #############################################

        class Context : IContext, IContextWritable
        {
            ICharactersContainer _charactersContainer;
            IField _field;
            IEventManagerInternal _eventManager;

            // =======================================

            public Context(ICharactersContainer characterContainer, IField field, IEventManagerInternal eventManager)
            {
                _charactersContainer = characterContainer;
                _field = field;
                _eventManager = eventManager;
            }

            // ============== IContext ================

            IField IContext.Field => _field;

            // ========== IContextWritable ============

            IField IContextWritable.Field => _field;
            ICharactersContainer IContextWritable.CharactardsContainer { get { return _charactersContainer; } set { _charactersContainer = value; }  }
            IEventManagerInternal IContextWritable.EventManager => _eventManager;
        }
    }
}