﻿namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Element { get; set; }

            public Node Next { get; set; }

            public Node(T element, Node next)
            {
                this.Element = element;
                this.Next = next;
            }

            public Node(T element)
                : base()
            {
                this.Element = element;
            }
        }

        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node(item, this.head);
            this.head = node;
            Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);

            if (this.head == null)
            {
                this.head = newNode;
            }
            else
            {
                var node = this.head;

                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            var node = this.head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Element;
        }

        public T RemoveFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            var oldHead = this.head;
            this.head = oldHead.Next;

            this.Count--;
            return oldHead.Element;
        }

        public T RemoveLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            //if (this.Count == 1)
            //{
            //    var currNode = this.head.Element;
            //    this.head = null;
            //    this.Count--;
            //    return currNode;
            //}

            //var node = this.head;

            //while (node.Next.Next != null)
            //{
            //    node = node.Next;
            //}

            //var currentNode = node.Element;
            //node.Next = null;

            //this.Count--;
            //return currentNode;

            Node current = this.head;
            Node last = null;
            // Works only for 1 element in the list
            if (current.Next == null)
            {
                last = this.head;
                this.head = null;
            }
            else
            {
                while (current != null)
                {
                    // Works if there are more than 1 elements in the list
                    if (current.Next.Next == null)
                    {
                        last = current.Next;
                        current.Next = null;
                        break;
                    }

                    current = current.Next;
                }
            }

            this.Count--;
            return last.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;

            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}