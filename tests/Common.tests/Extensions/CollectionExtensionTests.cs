using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using FluentAssertions;
using Xunit;

namespace Common.tests.Extensions {
    public class CollectionExtensionTests {
        private List<int> GetList(int count) {
            var r = new Random();
            var result = new List<int>();
            
            for (int i = 0; i < count; i++) {
                result.Add(r.Next());
            }

            return result;
        }
        
        [Fact]
        public void CanChunkCollection() {
            //Arrange
            var listToChunk = GetList(100);

            //Act
            var chunks = listToChunk.Chunk(10);

            //Assert
            chunks.Should().NotBeEmpty();
        }
        
        [Fact]
        public void ChunkingCollectionReturnsCorrectAmountForPerfectChunks() {
            //Arrange
            var listToChunk = GetList(100);

            //Act
            var chunks = listToChunk.Chunk(10);

            //Assert
            chunks.Should().HaveCount(10);
        }
        
        [Fact]
        public void ChunkingCollectionReturnsCorrectAmountForImPerfectChunks() {
            //Arrange
            var listToChunk = GetList(105);

            //Act
            var chunks = listToChunk.Chunk(10);

            //Assert
            chunks.Should().HaveCount(11);
        }
        
        [Fact]
        public void ChunkSizeIsCorrect() {
            //Arrange
            var listToChunk = GetList(20);

            //Act
            var chunks = listToChunk.Chunk(10);

            //Assert
            chunks.First().Should().HaveCount(10);
        }
        
        [Fact]
        public void LastChunkSizeHasLeftOverSize() {
            //Arrange
            var listToChunk = GetList(25);

            //Act
            var chunks = listToChunk.Chunk(10);

            //Assert
            chunks.Last().Should().HaveCount(5);
        }
        
        [Fact]
        public void ChunkingWith2ShouldDeliverOneChunk() {
            //Arrange
            var listToChunk = GetList(2);

            //Act
            var chunks = listToChunk.Chunk(100);

            //Assert
            chunks.Should().HaveCount(1);
        }
        
        
        [Fact]
        public void ChunkWithNoSizeThrowsException() {
            //Arrange
            var listToChunk = GetList(100);

            //Act
            Action act = () => listToChunk.Chunk(0).ToList();

            //Assert
            act.Should().Throw<InvalidOperationException>();
        }
        
        
        [Fact]
        public void SourceWithNoItemsResultInNoChunks() {
            //Arrange
            var listToChunk = GetList(0);

            //Act
            var chunks = listToChunk.Chunk(100);

            //Assert
            chunks.Should().HaveCount(0);
        }
        
        [Fact]
        public void SourceListIsNullReturnsException() {
            //Arrange
            List<int> listToChunk = null;

            //Act
            Action act = () => listToChunk.Chunk(5).ToList();

            //Assert
            act.Should().Throw<NullReferenceException>();
        }
    }
}
