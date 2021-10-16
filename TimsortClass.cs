using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timsort
{
    class TimsortClass
    {
        private int [] _array;

        public TimsortClass(int[] array)
        {
            this._array = array;
        }

        public TimsortClass(int length)
        {
            Random randomizer = new Random();
            _array = new int[length];

            for (int i = 0; i < length; i++)
            {
                _array[i] = randomizer.Next(-100, 100);
            }
        }
        
        public void Test()
        {
        }
        private void Swap(int indexF, int indexSec)
        {
            int tmp = _array[indexF];
            _array[indexF] = _array[indexSec];
            _array[indexSec] = tmp;
        }

        public void WriteArray()
        {
            Console.WriteLine();
            for (int i = 0; i < _array.Length; i++)
            {
                Console.Write($"{_array[i]}\t");
                if (i != 0 && i%10 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }


        public void Sort()
        {
            WriteArray();
            if (_array.Length <= 64)
            {
                InsertSort(0, _array.Length - 1);
                WriteArray();
                return;
            }
           
            SplitIntoRunsAndSortThem(GetMinrun());

            WriteArray();

        }

       
        private void InsertSort(int indexLeft, int indexRight)
        {
            //Console.WriteLine();
           //Console.WriteLine($"ind left: {indexLeft} ind right: {indexRight}");
            for (int i = indexLeft; i <= indexRight; i++)
            {
                int key = _array[i];
                int j = i;
                while ((j > indexLeft) && (_array[j - 1] > key))
                {
                    Swap(j - 1, j);
                    j--;
                }
                _array[j] = key;
            }
            //Console.WriteLine();
           // Console.WriteLine("Sorted Array: ");
            //for (int i = indexLeft; i <= indexRight; i++)
            //{
           //     Console.Write(_array[i]);
           // }
           // Console.WriteLine();
            //Console.WriteLine();
        }

        private void ReverseArray(int indexLeft, int indexRight)
        {
            int length = (indexRight - indexLeft) / 2;
            for (int i = indexLeft; i < length; i++)
            {
                Swap(i, length - 1 - i);
            }
        }

        private void SplitIntoRunsAndSortThem(int minRun)
        {
            int pointer = 0;
            bool isAscending = true;
            int run;
            StackClass stack = new StackClass();
            while (pointer < _array.Length)
            {
                run = 1;
                if (pointer != _array.Length - 1)
                {

                    if (_array[pointer] > _array[pointer + 1]) isAscending = false;
                    int index = pointer;

                    while ((_array[index] < _array[index + 1] && isAscending) ||
                        (_array[index] > _array[index + 1] && !isAscending))
                    {
                        run += 1;
                        index += 1;
                    }

                    if (!isAscending)
                    {
                        ReverseArray(pointer, pointer + run - 1);
                    }

                    if (run < minRun && (pointer + minRun <= _array.Length))
                    {
                        InsertSort(pointer, pointer + minRun - 1);
                        run = minRun;
                    }
                }
                //put in stack
                Structure data = new Structure(pointer, run);
                stack.Push(data);

                pointer += run;

                MergeDecision(stack);
            }
        }

        private void MergeDecision(StackClass stack)
        {
            Structure X;
            Structure Y;
            Structure Z;
            bool stop = false;
            while (!stop)
            {
                switch (stack.AmountOfEl())
                {
                    case 1:
                        //doNothing
                        stop = true;
                        break;
                    case 2:
                        X = stack.Pop();
                        Y = stack.Pop();

                        if (X.GetLength() > Y.GetLength())
                        {
                            X = Merge(Y, X);
                            stack.Push(X);
                        }
                        else
                        {
                            stack.Push(Y);
                            stack.Push(X);
                        }
                        stop = true;
                        break;
                    default:
                        X = stack.Pop();
                        Y = stack.Pop();
                        Z = stack.Pop();

                        if (X.GetLength() > Y.GetLength() || Z.GetLength() < (X.GetLength() + Y.GetLength()))
                        {
                            if (X.GetLength() <= Z.GetLength())
                            {
                                X = Merge(Y, X);
                                stack.Push(Z);
                                stack.Push(X);
                            }
                            else
                            {
                                Y = Merge(Z, Y);
                                stack.Push(Y);
                                stack.Push(X);
                            }
                        }
                        else
                        {
                            stack.Push(Y);
                            stack.Push(X);
                            stack.Push(Z);
                            stop = true;
                        }
                        break;
                }
            }
        }

        private Structure Merge(Structure Y, Structure X)
        {
            Structure new_structure = new Structure(Y.GetIndex(), Y.GetLength() + X.GetLength());
            int lenLeft = Y.GetLength();
            int[] left = new int[lenLeft];
            int lenRight = X.GetLength();
            int[] right = new int[lenRight];

            int[] result = new int[lenLeft + lenRight];

            int i = 0;
            int j = 0;
            int k = 0;

            //Console.WriteLine();
            //Console.Write("left: ");
            for (i = Y.GetIndex(), j = 0; i < Y.GetIndex() + lenLeft; j ++, i++)
            {
                left[j] = _array[i];
                //Console.Write($"{left[j]} ");
            }
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.Write("right: ");

            for (i = X.GetIndex(), j = 0; i < X.GetIndex() + lenRight; j++, i++)
            {
                right[j] = _array[i];
                //Console.Write($"{right[j]} ");
            }
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.Write("Result: ");

            i = 0;
            j = 0;
            k = 0;
            while (i < lenLeft && j < lenRight)
            {
                if (left[i] <= right[j])
                {
                    result[k] = left[i];
                    i++;
                }
                else
                {
                    result[k] = right[j];
                    j++;
                }
                //Console.Write($"{result[k]} ");
                k++;
            }
            //Console.WriteLine();
            //Console.WriteLine();

            // Copy remaining elements
            // of left, if any
            while (i < lenLeft)
            {
                result[k] = left[i];
                k++;
                i++;
            }

            // Copy remaining element
            // of right, if any
            while (j < lenRight)
            {
                result[k] = right[j];
                k++;
                j++;
            }

            //combine result and _array
            for (i = Y.GetIndex(), k = 0; i < lenRight + lenLeft; k++, i++)
            {
                _array[i] = result[k];
            }

            return new_structure;
        }

        /// <summary>
        /// Число minrun - минимальный размер упорядоченной последовательности.
        /// </summary>
        /// <returns>minrun</returns>
        private int GetMinrun()
        {
            int length = _array.Length;
            int r = 0;           /* станет 1 если среди сдвинутых битов будет хотя бы 1 ненулевой */
            while (length >= 64) //Оптимальная величина для N / minrun это степень числа 2 (или близким к нему).
                                      //наиболее эффективно использовать значения из диапазона (32;65)
            {
                r |= length & 1;
                length >>= 1;
            }
            return length + r;
        }


    }
}
