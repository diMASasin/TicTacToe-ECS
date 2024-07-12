using System.Collections.Generic;
using Leopotam.EcsLite;
using NUnit.Framework;
using TicTacToe.Components;
using TicTacToe.Systems;
using UnityEngine;

namespace TicTacToe.Editor
{
    [TestFixture]
    public class GameLogicTests
    {
        private EcsWorld CreateWorld(out Dictionary<Vector2Int, int> cells, out EcsPool<Taken> takenPool)
        {
            var world = new EcsWorld();

            cells = new Dictionary<Vector2Int, int>()
            {
                { new Vector2Int(0, 0), CreateCell(world, new Vector2Int(0, 0)) },
                { new Vector2Int(0, 1), CreateCell(world, new Vector2Int(0, 1)) },
                { new Vector2Int(0, 2), CreateCell(world, new Vector2Int(0, 2)) },
                { new Vector2Int(1, 0), CreateCell(world, new Vector2Int(1, 0)) },
                { new Vector2Int(1, 1), CreateCell(world, new Vector2Int(1, 1)) },
                { new Vector2Int(1, 2), CreateCell(world, new Vector2Int(1, 2)) },
                { new Vector2Int(2, 0), CreateCell(world, new Vector2Int(2, 0)) },
                { new Vector2Int(2, 1), CreateCell(world, new Vector2Int(2, 1)) },
                { new Vector2Int(2, 2), CreateCell(world, new Vector2Int(2, 2)) },
            };

            takenPool = world.GetPool<Taken>();
            return world;
        }

        [Test]
        public void CheckHorizontalChain()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            int chainLength = GameExtensions.GetLongestChain(cells, Vector2Int.zero, takenPool);

            Assert.AreEqual(0, chainLength);
        }

        [Test]
        public void CheckHorizontalChainOne()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[Vector2Int.zero]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, Vector2Int.zero, takenPool);

            Assert.AreEqual(1, chainLength);
        }

        [Test]
        public void CheckHorizontalChainTwoLeft()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[new Vector2Int(2, 0)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(1, 0)]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, new Vector2Int(2, 0), takenPool);

            Assert.AreEqual(2, chainLength);
        }
        
        [Test]
        public void CheckHorizontalChainTwoRight()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[new Vector2Int(2, 0)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(1, 0)]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, new Vector2Int(1, 0), takenPool);

            Assert.AreEqual(2, chainLength);
        }
        
        [Test]
        public void CheckVerticalChainTwo()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[new Vector2Int(0, 0)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(0, 1)]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, new Vector2Int(0, 0), takenPool);

            Assert.AreEqual(2, chainLength);
        }
        
        [Test]
        public void CheckVerticalChainThree()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[new Vector2Int(0, 0)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(0, 1)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(0, 2)]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, new Vector2Int(0, 0), takenPool);

            Assert.AreEqual(3, chainLength);
        }
        
        [Test]
        public void CheckDiagonalChainThree()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[new Vector2Int(0, 0)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(1, 1)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(2, 2)]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, new Vector2Int(1, 1), takenPool);

            Assert.AreEqual(3, chainLength);
        }
        
        [Test]
        public void CheckSecondDiagonalChainThree()
        {
            var world = CreateWorld(out var cells, out var takenPool);

            takenPool.Add(cells[new Vector2Int(0, 2)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(1, 1)]).Value = SignType.Cross;
            takenPool.Add(cells[new Vector2Int(2, 0)]).Value = SignType.Cross;
            
            int chainLength = GameExtensions.GetLongestChain(cells, new Vector2Int(1, 1), takenPool);

            Assert.AreEqual(3, chainLength);
        }

        private int CreateCell(EcsWorld world, Vector2Int position)
        {
            int entity = world.NewEntity();
            world.GetPool<Position>().Add(entity).Value = position;
            world.GetPool<Cell>().Add(entity);

            return entity;
        }
    }
}