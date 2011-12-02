using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SuperQTest
    {
        public SuperQTest()
        {
        }

        [TestMethod]
        public void GetQueue()
        {
            var testQueue = SuperQ.SuperQ<TestClass>.GetQueue("Queue");

            Assert.AreNotEqual(null, testQueue);
        }

        [TestMethod]
        public void PushMessage()
        {
            var message = new SuperQ.QueueMessage<TestClass>()
            {
                Payload = new TestClass
                {
                    word = "anthony",
                    number = 7
                }
            };

            var testQueue = SuperQ.SuperQ<TestClass>.GetQueue("Queue");
            testQueue.PushMessage(message);
        }


        [TestMethod]
        public void GetMessage()
        {
            var testQueue = SuperQ.SuperQ<TestClass>.GetQueue("Queue");
            var message = testQueue.GetMessage();

            Assert.AreEqual("anthony", message.Payload.word);
            Assert.AreEqual(7, message.Payload.number);
        }

        [TestMethod]
        public void GetAllMessages()
        {
            var queue = SuperQ.SuperQ<TestClass>.GetQueue("GetAllMessagesTestQueue");
            queue.Clear();
            var payload1 = new TestClass();
            payload1.word = "word1";
            payload1.number = 1;
            var payload2 = new TestClass();
            payload2.word = "word2";
            payload2.number = 2;
            queue.PushMessage(payload1);
            queue.PushMessage(payload2);
            var result = new List<TestClass>();
            foreach (var message in queue.GetAllMessages())
            {
                result.Add(message.Payload);
            }

            Assert.AreEqual(result.Count, 2);
            
            Assert.AreEqual(result[0], payload1);
            Assert.AreEqual(result[1], payload2);

            queue.Delete();
        }

        [TestMethod]
        public void DeleteMessage()
        {
            var testQueue = SuperQ.SuperQ<TestClass>.GetQueue("Queue");
            var message = testQueue.GetMessage();

            testQueue.DeleteMessage(message);

            //TODO: Assert message is deleted
        }

        [TestMethod]
        public void ClearQueue()
        {
            var testQueue = SuperQ.SuperQ<TestClass>.GetQueue("Queue");
            testQueue.Clear();
        }

        [TestMethod]
        public void DeleteQueue()
        {
            var testQueue = SuperQ.SuperQ<TestClass>.GetQueue("Queue");
            testQueue.Delete();

            //TODO: Assert database is deleted
        }
    
    }

    public class TestClass
    {
        public string word { get; set; }
        public int number { get; set; }

        public override bool Equals(object obj)
        {
            return this.word == ((TestClass)obj).word && this.number == ((TestClass)obj).number;
        }

        public override int GetHashCode()
        {
            return word.GetHashCode() * number;
        }
    }
}
