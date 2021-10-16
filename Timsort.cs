﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timsort
{
    class Timsort
    {
        private int [] _array;

        public Timsort(int[] array)
        {
            this._array = array;
        }

        public Timsort(int length)
        {
            Random randomizer = new Random();
            _array = new int[length];

            for (int i = 0; i < length; i++)
            {
                _array[i] = randomizer.Next(-100, 100);
            }
        }
        

        private void Swap(int indexF, int indexSec)
        {
            int tmp = _array[indexF];
            _array[indexF] = _array[indexSec];
            _array[indexSec] = tmp;
        }

        public void WriteArray()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                Console.Write($"{_array[i]}\t");
            }
            Console.WriteLine();
            for (int i = 0; i < _array.Length; i++)
            {
                Console.Write($"{i}\t");
            }
            Console.WriteLine();
        }


        public void Sort()
        {
            WriteArray();
            if (_array.Length <= 64)
            {
                //Простая сортировка вставками
            }
            GetMinrun();


        }

        private void Insert(int element, int position, int indexRight)
        {
            for (int i = indexRight + 1; i > position; i++)
            {
                _array[i] = _array[i - 1];
            }
            _array[position] = element;
        }

        private void BinarySearch(int indexLeft, int indexRight, int element, bool isAscending)
        {
            int left = indexLeft;
            int right = indexRight;
            while (left <= right)
            {
                int middle = (left + right) / 2;
                
                if ((_array[middle] < element && element < _array[middle + 1] && isAscending) ||
                    (_array[middle] > element && element > _array[middle + 1] && !isAscending))//вставляем элемент
                {
                    Insert(element, middle + 1, indexRight);
                    return;
                }
                
                if ((_array[middle] < element && element > _array[middle + 1] && isAscending) ||
                    (_array[middle] > element && element < _array[middle + 1] && !isAscending))
                {
                    left += 1;
                }

                if ((_array[middle] > element && isAscending)||
                    (_array[middle] < element && !isAscending))
                {
                    if (middle == indexLeft) Insert(element, indexLeft, indexRight);
                    right -= 1;
                }
            }
        }

        /// <summary>
        /// Число minrun - минимальный размер упорядоченной последовательности.
        /// </summary>
        /// <returns>minrun</returns>
        private int GetMinrun()
        {
            int length = _array.Length;
            int r = 0;           /* станет 1 если среди сдвинутых битов будет хотя бы 1 ненулевой */
            while (_array.Length >= 64) //Оптимальная величина для N / minrun это степень числа 2 (или близким к нему).
                                      //наиболее эффективно использовать значения из диапазона (32;65)
            {
                r |= length & 1;
                length >>= 1;
            }
            return length + r;
        }


    }
}
