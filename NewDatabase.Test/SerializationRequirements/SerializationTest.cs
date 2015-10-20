using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Newtonsoft.Json;
using Xunit;

namespace NewDatabase.Test.SerializationRequirements
{
    public class SerializationTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        public SerializationTest()
        {
            _dataTest = new DataTest.DataTest();
            _index = new Index();
            _relation = new Relation();
        }

        [Fact]
        public void ShouldSerializeOneToOne()
        {
            var index = new Index();
            var relation = new Relation();

            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id, relation, index);
            var geometryTable = new Table<Geometry>(_dataTest.Geometries, g => g.Id, relation, index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            wellTable.Insert(well);


            var jsonData = JsonConvert.SerializeObject(_dataTest, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(index, Formatting.Indented);

            var newData = JsonConvert.DeserializeObject<DataTest.DataTest>(jsonData);
            var newIndex = JsonConvert.DeserializeObject<Index>(jsonIndex);
            var newRelation = new Relation();


            var newWellTable = new Table<Well>(newData.Wells, w => w.Id, newRelation, newIndex);
            var newGeometryTable = new Table<Geometry>(newData.Geometries, g => g.Id, newRelation, newIndex);

            newRelation.CreateOneToOne(newWellTable, newGeometryTable, w => w.Geometry.Id);

            Assert.True(1 == newWellTable.Count);
            Assert.True(1 == newGeometryTable.Count);
            Assert.True(2 == newIndex.Count);

            Assert.Equal(well.Id, newWellTable.Get(well.Id).Id);
            Assert.Equal(geometry.Id, newWellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, newGeometryTable.Get(geometry.Id).Id);

            newWellTable.Delete(well);

            Assert.True(0 == newWellTable.Count);
            Assert.True(0 == newGeometryTable.Count);
            Assert.True(0 == newIndex.Count);
        }

        [Fact]
        public void ShouldSerializeOneToMany()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);

            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            trajectoryTable.Insert(trajectory);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);

            var jsonData = JsonConvert.SerializeObject(_dataTest, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(_index, Formatting.Indented);

            var newData = JsonConvert.DeserializeObject<DataTest.DataTest>(jsonData);
            var newIndex = JsonConvert.DeserializeObject<Index>(jsonIndex);
            var newRelation = new Relation();

            var newTrajectoryTable = new Table<Trajectory>(newData.Trajectories, t => t.Id, newRelation, newIndex);
            var newTrajectoryPointTable = new Table<TrajectoryPoint>(newData.TrajectoryPoints, tp => tp.Id, newRelation,
                newIndex);

            newRelation.CreateOneToMany(newTrajectoryTable, newTrajectoryPointTable, tp => tp.Trajectory.Id);

            Assert.True(1 == newTrajectoryTable.Count);
            Assert.True(3 == newTrajectoryPointTable.Count);

            newTrajectoryTable.Delete(trajectory);

            Assert.True(0 == newTrajectoryTable.Count);
            Assert.True(0 == newTrajectoryPointTable.Count);
        }

        [Fact(DisplayName = "Create a common scenario, serialize and cascate delete")]
        public void ShouldSerializeWithOneToManyAndOneToOne()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id, _relation, _index);
            var geometryTable = new Table<Geometry>(_dataTest.Geometries, g => g.Id, _relation, _index);
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);

            _relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);
            _relation.CreateOneToOne(wellTable, trajectoryTable, w => w.Trajectory.Id);
            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var geometry = new Geometry();
            var trajectory = new Trajectory();

            var well = new Well(geometry, trajectory);

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            geometryTable.Insert(geometry);
            trajectoryTable.Insert(trajectory);
            wellTable.Insert(well);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
            Assert.True(1 == trajectoryTable.Count);
            Assert.True(3 == trajectoryPointTable.Count);


            var jsonData = JsonConvert.SerializeObject(_dataTest, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(_index, Formatting.Indented);

            var newData = JsonConvert.DeserializeObject<DataTest.DataTest>(jsonData);
            var newIndex = JsonConvert.DeserializeObject<Index>(jsonIndex);
            var newRelation = new Relation();


            var newWellTable = new Table<Well>(newData.Wells, w => w.Id, newRelation, newIndex);
            var newGeometryTable = new Table<Geometry>(newData.Geometries, g => g.Id, newRelation, newIndex);
            var newTrajectoryTable = new Table<Trajectory>(newData.Trajectories, t => t.Id, newRelation, newIndex);
            var newTrajectoryPointTable = new Table<TrajectoryPoint>(newData.TrajectoryPoints, tp => tp.Id, newRelation,
                newIndex);

            newRelation.CreateOneToOne(newWellTable, newGeometryTable, w => w.Geometry.Id);
            newRelation.CreateOneToOne(newWellTable, newTrajectoryTable, w => w.Trajectory.Id);
            newRelation.CreateOneToMany(newTrajectoryTable, newTrajectoryPointTable, tp => tp.Trajectory.Id);

            Assert.True(1 == newWellTable.Count);
            Assert.True(1 == newGeometryTable.Count);
            Assert.True(1 == newTrajectoryTable.Count);
            Assert.True(3 == newTrajectoryPointTable.Count);

            newWellTable.Delete(well);

            Assert.True(0 == newWellTable.Count);
            Assert.True(0 == newGeometryTable.Count);
            Assert.True(0 == newTrajectoryTable.Count);
            Assert.True(0 == newTrajectoryPointTable.Count);
        }

        [Fact]
        public void ShouldSerializeManyToMany()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            var jsonData = JsonConvert.SerializeObject(_dataTest, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(_index, Formatting.Indented);

            var newData = JsonConvert.DeserializeObject<DataTest.DataTest>(jsonData);
            var newIndex = JsonConvert.DeserializeObject<Index>(jsonIndex);
            var newRelation = new Relation();

            var newTrajectoryTable = new Table<Trajectory>(newData.Trajectories, t => t.Id, newRelation, newIndex);
            var newGraphicTable = new Table<Graphic>(newData.Graphics, g => g.Id, newRelation, newIndex);
            var newTrajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(newData.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    newRelation, newIndex);

            newRelation.CreateManyToMany(newTrajectoryTable, newGraphicTable, newTrajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            newTrajectoryTable.Delete(trajectory);

            Assert.True(0 == newTrajectoryTable.Count);
            Assert.True(1 == newGraphicTable.Count);
            Assert.True(0 == newTrajectoryGraphicRelationalTable.Count);
        }

        [Fact]
        public void ShouldSerializeWithOneToManyAndOneToOneAndManyToMany()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id, _relation, _index);
            var geometryTable = new Table<Geometry>(_dataTest.Geometries, g => g.Id, _relation, _index);
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            _relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);
            _relation.CreateOneToOne(wellTable, trajectoryTable, w => w.Trajectory.Id);
            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);
            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);


            var geometry = new Geometry();
            var trajectory = new Trajectory();
            var well = new Well(geometry, trajectory);

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);


            trajectoryTable.Insert(trajectory);
            geometryTable.Insert(geometry);
            wellTable.Insert(well);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);


            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
            Assert.True(1 == trajectoryTable.Count);
            Assert.True(3 == trajectoryPointTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            var jsonData = JsonConvert.SerializeObject(_dataTest, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(_index, Formatting.Indented);

            var newData = JsonConvert.DeserializeObject<DataTest.DataTest>(jsonData);
            var newIndex = JsonConvert.DeserializeObject<Index>(jsonIndex);
            var newRelation = new Relation();

            var newWellTable = new Table<Well>(newData.Wells, w => w.Id, newRelation, newIndex);
            var newGeometryTable = new Table<Geometry>(newData.Geometries, g => g.Id, newRelation, newIndex);
            var newTrajectoryTable = new Table<Trajectory>(newData.Trajectories, t => t.Id, newRelation, newIndex);
            var newTrajectoryPointTable = new Table<TrajectoryPoint>(newData.TrajectoryPoints, tp => tp.Id, newRelation,
                newIndex);
            var newGraphicTable = new Table<Graphic>(newData.Graphics, g => g.Id, newRelation, newIndex);
            var newTrajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(newData.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    newRelation, newIndex);

            newRelation.CreateOneToOne(newWellTable, newGeometryTable, w => w.Geometry.Id);
            newRelation.CreateOneToOne(newWellTable, newTrajectoryTable, w => w.Trajectory.Id);
            newRelation.CreateOneToMany(newTrajectoryTable, newTrajectoryPointTable, tp => tp.Trajectory.Id);
            newRelation.CreateManyToMany(newTrajectoryTable, newGraphicTable, newTrajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            newWellTable.Delete(well);

            Assert.True(0 == newWellTable.Count);
            Assert.True(0 == newGeometryTable.Count);
            Assert.True(0 == newTrajectoryTable.Count);
            Assert.True(0 == newTrajectoryPointTable.Count);
            Assert.True(1 == newGraphicTable.Count);
            Assert.True(0 == newTrajectoryGraphicRelationalTable.Count);
        }
    }
}