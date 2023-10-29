using System;

public class Quaternion
{
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Quaternion(double w, double x, double y, double z)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }

    // Додавання кватерніонів
    public static Quaternion operator +(Quaternion a, Quaternion b)
    {
        return new Quaternion(a.W + b.W, a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    // Віднімання кватерніонів
    public static Quaternion operator -(Quaternion a, Quaternion b)
    {
        return new Quaternion(a.W - b.W, a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    // Множення кватерніонів
    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        double w = a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;
        double x = a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y;
        double y = a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X;
        double z = a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W;
        return new Quaternion(w, x, y, z);
    }

    // Обчислення норми кватерніона
    public double Norm()
    {
        return Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
    }

    // Обчислення спряженого кватерніона
    public Quaternion Conjugate()
    {
        return new Quaternion(W, -X, -Y, -Z);
    }

    // Обчислення інверсії кватерніона
    public Quaternion Inverse()
    {
        double normSquared = W * W + X * X + Y * Y + Z * Z;
        if (normSquared == 0)
            throw new InvalidOperationException("Неможливо інвертувати кватерніон з нульовою нормою.");

        double scale = 1.0 / normSquared;
        return new Quaternion(W * scale, -X * scale, -Y * scale, -Z * scale);
    }

    // Перевантажені операції порівняння
    public static bool operator ==(Quaternion a, Quaternion b)
    {
        return a.W == b.W && a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }

    public static bool operator !=(Quaternion a, Quaternion b)
    {
        return !(a == b);
    }

    // Метод для конвертації кватерніона в матрицю обертання
    public double[,] ToRotationMatrix()
    {
        double xx = X * X;
        double xy = X * Y;
        double xz = X * Z;
        double xw = X * W;
        double yy = Y * Y;
        double yz = Y * Z;
        double yw = Y * W;
        double zz = Z * Z;
        double zw = Z * W;

        double[,] matrix = new double[3, 3];
        matrix[0, 0] = 1 - 2 * (yy + zz);
        matrix[0, 1] = 2 * (xy - zw);
        matrix[0, 2] = 2 * (xz + yw);
        matrix[1, 0] = 2 * (xy + zw);
        matrix[1, 1] = 1 - 2 * (xx + zz);
        matrix[1, 2] = 2 * (yz - xw);
        matrix[2, 0] = 2 * (xz - yw);
        matrix[2, 1] = 2 * (yz + xw);
        matrix[2, 2] = 1 - 2 * (xx + yy);

        return matrix;
    }

    // Метод для конвертації матриці обертання в кватерніон
    public static Quaternion FromRotationMatrix(double[,] matrix)
    {
        if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
        {
            throw new ArgumentException("Матриця обертання повинна бути розміром 3x3.");
        }

        double trace = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];
        double w, x, y, z;

        if (trace > 0)
        {
            double s = 0.5 / Math.Sqrt(trace + 1.0);
            w = 0.25 / s;
            x = (matrix[2, 1] - matrix[1, 2]) * s;
            y = (matrix[0, 2] - matrix[2, 0]) * s;
            z = (matrix[1, 0] - matrix[0, 1]) * s;
        }
        else if (matrix[0, 0] > matrix[1, 1] && matrix[0, 0] > matrix[2, 2])
        {
            double s = 2.0 * Math.Sqrt(1.0 + matrix[0, 0] - matrix[1, 1] - matrix[2, 2]);
            w = (matrix[2, 1] - matrix[1, 2]) / s;
            x = 0.25 * s;
            y = (matrix[0, 1] + matrix[1, 0]) / s;
            z = (matrix[0, 2] + matrix[2, 0]) / s;
        }
        else if (matrix[1, 1] > matrix[2, 2])
        {
            double s = 2.0 * Math.Sqrt(1.0 + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
            w = (matrix[0, 2] - matrix[2, 0]) / s;
            x = (matrix[0, 1] + matrix[1, 0]) / s;
            y = 0.25 * s;
            z = (matrix[1, 2] + matrix[2, 1]) / s;
        }
        else
        {
            double s = 2.0 * Math.Sqrt(1.0 + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
            w = (matrix[1, 0] - matrix[0, 1]) / s;
            x = (matrix[0, 2] + matrix[2, 0]) / s;
            y = (matrix[1, 2] + matrix[2, 1]) / s;
            z = 0.25 * s;
        }

        return new Quaternion(w, x, y, z);
    }
}

class Program
{
    static void Main()
    {
        Quaternion q1 = new Quaternion(1, 2, 3, 4);
        Quaternion q2 = new Quaternion(5, 6, 7, 8);

        Console.WriteLine("Кватерніон 1: " + q1.W + " + " + q1.X + "i + " + q1.Y + "j + " + q1.Z + "k");
        Console.WriteLine("Кватерніон 2: " + q2.W + " + " + q2.X + "i + " + q2.Y + "j + " + q2.Z + "k");

        Quaternion додавання = q1 + q2;
        Console.WriteLine("Додавання: " + додавання.W + " + " + додавання.X + "i + " + додавання.Y + "j + " + додавання.Z + "k");

        Quaternion віднімання = q1 - q2;
        Console.WriteLine("Віднімання: " + віднімання.W + " + " + віднімання.X + "i + " + віднімання.Y + "j + " + віднімання.Z + "k");

        Quaternion множення = q1 * q2;
        Console.WriteLine("Множення: " + множення.W + " + " + множення.X + "i + " + множення.Y + "j + " + множення.Z + "k");

        double норма = q1.Norm();
        Console.WriteLine("Норма кватерніона 1: " + норма);

        Quaternion спряжений = q1.Conjugate();
        Console.WriteLine("Спряжений кватерніон 1: " + спряжений.W + " + " + спряжений.X + "i + " + спряжений.Y + "j + " + спряжений.Z + "k");

        Quaternion інверсія = q1.Inverse();
        Console.WriteLine("Інверсія кватерніона 1: " + інверсія.W + " + " + інверсія.X + "i + " + інверсія.Y + "j + " + інверсія.Z + "k");

        bool рівні = (q1 == q2);
        Console.WriteLine("Чи рівні Кватерніон 1 та Кватерніон 2? " + рівні);

        double[,] матрицяОбертання = q1.ToRotationMatrix();
        Console.WriteLine("Матриця Обертання для Кватерніон 1:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(матрицяОбертання[i, j] + " ");
            }
            Console.WriteLine();
        }

        Quaternion зМатриці = Quaternion.FromRotationMatrix(матрицяОбертання);
        Console.WriteLine("Кватерніон із Матриці Обертання:");
        Console.WriteLine("W: " + зМатриці.W);
        Console.WriteLine("X: " + зМатриці.X);
        Console.WriteLine("Y: " + зМатриці.Y);
        Console.WriteLine("Z: " + зМатриці.Z);
    }
}