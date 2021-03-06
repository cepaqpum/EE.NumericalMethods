﻿using System;

namespace EE.NumericalMethods.Core.Common.Builders.MathNet3
{
    public class MathNet3
    {
        /// <summary>
        /// Максимальное значение по пространству
        /// </summary>
        public double MaxX { get; }

        public double MaxY { get; }

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

        /// <summary>
        /// Размер сетки по 1 пространству
        /// </summary>
        public int SizeX { get; }

        /// <summary>
        /// Размер сетки по 2 пространству
        /// </summary>
        public int SizeY { get; }

        //Внутреннее поле с сеткой
        public readonly double[,,] Net;
        
        /// <summary>
        /// Размер сетки по времени
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Установить значение в ячейку сетки
        /// </summary>
        /// <param name="i">Номер ячейки по 1 пространству</param>
        /// <param name="j">Номер ячейки по 2 пространству</param>
        /// <param name="k">Номер ячейки по времени</param>
        /// <param name="value">Значение</param>
        public void Set(int i, int j, int k, double value)
        {
            Net[i, j, k] = value;
        }
        
        /// <summary>
        /// Получить значение ячейки из сетки
        /// </summary>
        /// <param name="i">Номер ячейки по 1 пространству</param>
        /// <param name="j">Номер ячейки по 2 пространству</param>
        /// <param name="k">Номер ячейки по времени</param>
        /// <returns>Значение сетки в i,j</returns>
        public double Get(int i, int j, int k)
        {
            return Net[i, j, k];
        }

        /// <summary>
        /// Конструктор, инициализирующий сетку по основным параметрам
        /// </summary>
        /// <param name="maxX">максимум по 1 пространству</param>
        /// <param name="maxY">максимум по 2 пространству</param>
        /// <param name="maxT">максимум по времени</param>
        /// <param name="h">шаг по пространству</param>
        /// <param name="d">шаг по времени</param>
        public MathNet3(double maxX, double maxY, double maxT, double h, double d)
        {
            MaxX = maxX;
            MaxY = maxY;
            MaxT = maxT;
            H = h;
            D = d;
            SizeX = (int)(maxX / h);
            SizeY = (int)(maxY / h);
            Height = (int)(maxT / d);
            Net = new double[SizeX + 1, SizeY + 1, Height + 1];
        }

        /// <summary>
        /// Функция вычисления ошибки
        /// </summary>
        /// <param name="expected">Реальное значение</param>
        /// <returns></returns>
        public double GetError(Func<double, double, double, double> expected)
        {
            var result = 0d;
            for (var k = 0; k <= Height; k++)
            for (var j = 0; j <= SizeY; j++)
            for (var i = 0; i <= SizeX; i++)
            {
                var x = H * i;
                var y = H * j;
                var t = D * k;
                var error = Math.Abs(expected(x, y, t) - Get(i, j, k));
                if (error > result)
                {
                    result = error;
                }
            }
            return result;
        }
    }
}