using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    public static class Enumerator
    {
        public static void EnumeratorMain()
        {
            Console.WriteLine("Enumerator Main");

            // foreach 의 구성
            int[] arrValue = { 1, 2, 3, 4, 5, 6, 7 };
            MyCollection myCollection = new MyCollection(arrValue);

            foreach (var item in myCollection)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class MyCollection : IEnumerable
    {
        private int[] data;

        public MyCollection(int[] data)
        {
            this.data = data;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyCollectionEnumrator(this.data);
        }
    }

    public class MyCollectionEnumrator : IEnumerator
    {
        private int[] data;
        private int position = -1;

        public MyCollectionEnumrator(int[] data)
        {
            this.data = data;
        }

        public object Current
        {
            get
            {
                if (position == -1 || position >= data.Length)
                    throw new InvalidOperationException();
                return data[position];
            }
        }

        public bool MoveNext()
        {
            // throw new NotImplementedException();
            position++;
            return (position < data.Length);
        }

        public void Reset()
        {
            // throw new NotImplementedException();
            position = -1;
        }
    }
}