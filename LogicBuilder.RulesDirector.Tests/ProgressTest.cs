using System;

namespace LogicBuilder.RulesDirector.Tests
{
    public class ProgressTest
    {
        [Fact]
        public void Constructor_InitializesEmptyCollection()
        {
            //arrange & act
            var progress = new Progress();

            //assert
            Assert.NotNull(progress);
            Assert.NotNull(progress.ProgressItems);
            Assert.Empty(progress.ProgressItems);
        }

        [Fact]
        public void AddProgressItem_AddsItemToCollection()
        {
            //arrange
            var progress = new Progress();
            string description = "Test progress item";

            //act
            progress.AddProgressItem(description);

            //assert
            Assert.Single(progress.ProgressItems);
            Assert.Equal(description, progress.ProgressItems[0].Description);
        }

        [Fact]
        public void AddProgressItem_AddsMultipleItemsToCollection()
        {
            //arrange
            var progress = new Progress();
            string description1 = "First progress item";
            string description2 = "Second progress item";
            string description3 = "Third progress item";

            //act
            progress.AddProgressItem(description1);
            progress.AddProgressItem(description2);
            progress.AddProgressItem(description3);

            //assert
            Assert.Equal(3, progress.ProgressItems.Count);
            Assert.Equal(description1, progress.ProgressItems[0].Description);
            Assert.Equal(description2, progress.ProgressItems[1].Description);
            Assert.Equal(description3, progress.ProgressItems[2].Description);
        }

        [Fact]
        public void AddProgressItem_RaisesItemAddedEvent()
        {
            //arrange
            var progress = new Progress();
            string description = "Test progress item";
            bool eventRaised = false;
            ProgressInfo? eventItem = null;

            progress.ItemAdded += (sender, args) =>
            {
                eventRaised = true;
                eventItem = sender as ProgressInfo;
            };

            //act
            progress.AddProgressItem(description);

            //assert
            Assert.True(eventRaised);
            Assert.NotNull(eventItem);
            Assert.Equal(description, eventItem.Description);
        }

        [Fact]
        public void AddProgressItem_RaisesItemAddedEvent_WithCorrectArguments()
        {
            //arrange
            var progress = new Progress();
            string description = "Test progress item";
            object? eventSender = null;
            EventArgs? eventArgs = null;

            progress.ItemAdded += (sender, args) =>
            {
                eventSender = sender;
                eventArgs = args;
            };

            //act
            progress.AddProgressItem(description);

            //assert
            Assert.NotNull(eventSender);
            Assert.IsType<ProgressInfo>(eventSender);
            Assert.Same(EventArgs.Empty, eventArgs);
        }

        [Fact]
        public void AddProgressItem_RaisesItemAddedEvent_MultipleSubscribers()
        {
            //arrange
            var progress = new Progress();
            string description = "Test progress item";
            int eventCount = 0;

            progress.ItemAdded += (sender, args) => eventCount++;
            progress.ItemAdded += (sender, args) => eventCount++;
            progress.ItemAdded += (sender, args) => eventCount++;

            //act
            progress.AddProgressItem(description);

            //assert
            Assert.Equal(3, eventCount);
        }

        [Fact]
        public void ClearProgressList_ClearsAllItems()
        {
            //arrange
            var progress = new Progress();
            progress.AddProgressItem("Item 1");
            progress.AddProgressItem("Item 2");
            progress.AddProgressItem("Item 3");

            //act
            progress.ClearProgressList();

            //assert
            Assert.Empty(progress.ProgressItems);
        }

        [Fact]
        public void ClearProgressList_OnEmptyCollection_DoesNotThrow()
        {
            //arrange
            var progress = new Progress();

            //act
            var exception = Record.Exception(() => progress.ClearProgressList());

            //assert
            Assert.Null(exception);
        }

        [Fact]
        public void ClearProgressList_RaisesListClearedEvent()
        {
            //arrange
            var progress = new Progress();
            progress.AddProgressItem("Item 1");
            bool eventRaised = false;

            progress.ListCleared += (sender, args) =>
            {
                eventRaised = true;
            };

            //act
            progress.ClearProgressList();

            //assert
            Assert.True(eventRaised);
        }

        [Fact]
        public void ClearProgressList_RaisesListClearedEvent_WithCorrectArguments()
        {
            //arrange
            var progress = new Progress();
            progress.AddProgressItem("Item 1");
            object? eventSender = null;
            EventArgs? eventArgs = null;

            progress.ListCleared += (sender, args) =>
            {
                eventSender = sender;
                eventArgs = args;
            };

            //act
            progress.ClearProgressList();

            //assert
            Assert.Same(progress, eventSender);
            Assert.Same(EventArgs.Empty, eventArgs);
        }

        [Fact]
        public void ClearProgressList_RaisesListClearedEvent_MultipleSubscribers()
        {
            //arrange
            var progress = new Progress();
            progress.AddProgressItem("Item 1");
            int eventCount = 0;

            progress.ListCleared += (sender, args) => eventCount++;
            progress.ListCleared += (sender, args) => eventCount++;
            progress.ListCleared += (sender, args) => eventCount++;

            //act
            progress.ClearProgressList();

            //assert
            Assert.Equal(3, eventCount);
        }

        [Fact]
        public void ItemAdded_NotRaisedWithoutSubscription()
        {
            //arrange
            var progress = new Progress();
            string description = "Test progress item";

            //act
            var exception = Record.Exception(() => progress.AddProgressItem(description));

            //assert
            Assert.Null(exception);
            Assert.Single(progress.ProgressItems);
        }

        [Fact]
        public void ListCleared_NotRaisedWithoutSubscription()
        {
            //arrange
            var progress = new Progress();
            progress.AddProgressItem("Item 1");

            //act
            var exception = Record.Exception(() => progress.ClearProgressList());

            //assert
            Assert.Null(exception);
            Assert.Empty(progress.ProgressItems);
        }

        [Fact]
        public void ProgressItems_ReturnsSameCollectionInstance()
        {
            //arrange
            var progress = new Progress();

            //act
            var collection1 = progress.ProgressItems;
            var collection2 = progress.ProgressItems;

            //assert
            Assert.Same(collection1, collection2);
        }

        [Fact]
        public void AddProgressItem_AfterClear_AddsToEmptyCollection()
        {
            //arrange
            var progress = new Progress();
            progress.AddProgressItem("Item 1");
            progress.AddProgressItem("Item 2");
            progress.ClearProgressList();

            //act
            progress.AddProgressItem("New Item");

            //assert
            Assert.Single(progress.ProgressItems);
            Assert.Equal("New Item", progress.ProgressItems[0].Description);
        }
    }
}