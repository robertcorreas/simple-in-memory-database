using System;

namespace SimpleInMemoryDatabase.Lib.Api
{
    public class Entity
    {
        #region Construtores

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Propriedades

        public Guid Id { get; private set; }

        #endregion
    }
}