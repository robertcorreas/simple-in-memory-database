using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.TableTests
{
    public class TableInsertionTests : TestBase
    {
        [Fact(DisplayName = "Should Insert Well In Well Table")]
        public void ShouldInsertWellInWellTable()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.Insert(well);

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
        }

        [Fact(DisplayName = "Should Insert Well In Well Table And Inserted Is A Copy")]
        public void ShouldInsertWellInWellTableAndInsertedIsACopy()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.Insert(well);

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.NotEqual(well, Db.GetOne<Well>(well.Id));
        }

        [Fact(DisplayName = "Should Throw Exception If The Same Entity Is Inserted")]
        public void ShouldThrowExceptionIfTheSameEntityIsInserted()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.Insert(well);

            var ex = Assert.Throws<ArgumentException>(() => { Db.Insert(well); });

            Assert.Equal("Already exist an entity with this id", ex.Message);

            Assert.True(1 == Db.Count<Well>());
        }

        [Fact(DisplayName = "Should Insert If Update")]
        public void ShouldInsertOrUpdate()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.InsertOrUpdate(well);

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.NotEqual(well, Db.GetOne<Well>(well.Id));
        }

        [Fact(DisplayName = "Should update if not insert")]
        public void ShouldUpdateIfNotInsert()
        {
            var well = new Well(new Geometry(), new Trajectory());
            well.Nome = "Poço";
            Db.Insert(well);

            well.Nome = "Novo Poço";
            Db.InsertOrUpdate(well);

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.NotEqual(well, Db.GetOne<Well>(well.Id));
            Assert.Equal(well.Nome, Db.GetOne<Well>(well.Id).Nome);
        }
    }
}