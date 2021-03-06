﻿using System;
using System.Runtime.CompilerServices;

namespace EE.NumericalMethods.Core.Common.Builders.MathNet2
{
    public class MathNet2
    {
        /// <summary>
        /// Максимальное значение по пространству
        /// </summary>
        public double MaxX { get; }
        /// <summary>
        /// Максимальное значение по времени
        /// </summary>
        public double MaxT { get; }
        /// <summary>
        /// Шаг по пространству
        /// </summary>
        public double H { get; }
        /// <summary>
        /// Шаг по времени
        /// </summary>
        public double D { get; }

        //Внутреннее поле с сеткой
        public readonly double[,] Net;

        /// <summary>
        /// Размер сетки по пространству
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Размер сетки по времени
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Конструктор, инициализирующий сетку по основным параметрам
        /// </summary>
        /// <param name="maxX">максимум по пространству</param>
        /// <param name="maxT">максимум по времени</param>
        /// <param name="h">шаг по пространству</param>
        /// <param name="d">шаг по времени</param>
        public MathNet2(double maxX, double maxT, double h, double d)
        {
            MaxX = maxX;
            MaxT = maxT;
            H = h;
            D = d / 2;
            Width = (int)(maxX / h);
            Height = (int)(maxT / d);
            Net = new double[Width + 1, Height + 1];
        }

        /// <summary>
        /// Установить значение в ячейку сетки
        /// </summary>
        /// <param name="i">Номер ячейки по пространству</param>
        /// <param name="j">Номер ячейки по времени</param>
        /// <param name="value">Значение</param>
        public void Set(int i, int j, double value)
        {
            Net[i, j] = value;
        }

        /// <summary>
        /// Получить значение ячейки из сетки
        /// </summary>
        /// <param name="i">Номер ячейки по пространству</param>
        /// <param name="j">Номер ячейки по времени</param>
        /// <returns>Значение сетки в i,j</returns>
        public double Get(int i, int j)
        {
            return Net[i, j];
        }

        public double[] GetRow(int j)
        {
            var result = new double[Width +1 ];
            for (var i = 0 ; i  <= Width; i++)
            {
                result[i] = Get(i, j);
            }
            return result;
        }

        /// <summary>
        /// Функция вычисления ошибки
        /// </summary>
        /// <param name="expected">Реальное значение</param>
        /// <returns></returns>
        public double GetError(Func<double, double, double> expected)
        {
            var result = 0d;
            for (var j = 0; j <= Height; j++)
            {
                for (var i = 0; i <= Width; i++)
                {
                    var x = H * i;
                    var t = D * j;
                    var error = Math.Abs(expected(x, t) - Get(i, j));
                    if (error > result)
                    {
                        result = error;
                    }
                }
            }
            return result;
        }
    }
}