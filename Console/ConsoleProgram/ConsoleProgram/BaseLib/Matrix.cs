using System;
using System.Text.RegularExpressions;


public class Matrix
{
    public int rows;
    public int cols;
    public double[,] mat;

    public Matrix L;
    public Matrix U;
    private int[] pi;
    private double detOfP = 1;

    //CONSTRUCTOR
    public Matrix(int _rows, int _cols)
    {
        rows = _rows;
        cols = _cols;
        mat = new double[rows, cols];
    }

    public Boolean IsSquare()
    {
        return (rows == cols);
    }

    //ACCESSOR
    public double this[int _row, int _col]
    {
        get { return mat[_row, _col]; }
        set { mat[_row, _col] = value; }
    }

    public Matrix GetCol(int _k)
    {
        Matrix m = new Matrix(rows, 1);
        for (int i = 0; i < rows; i++) m[i, 0] = mat[i, _k];
        return m;
    }

    public void SetCol(Matrix _v, int _k)
    {
        for (int i = 0; i < rows; i++) mat[i, _k] = _v[i, 0];
    }
    public Matrix GetRow(int _k)
    {
        Matrix m = new Matrix(1, cols);
        for (int i = 0; i < cols; i++) m[0, i] = mat[_k, i];
        return m;
    }

    public void SetRow(Matrix _v, int _k)
    {
        for (int i = 0; i < cols; i++) mat[_k, i] = _v[0, i];
    }



    public void MakeLU()                        // Function for LU decomposition
    {
        if (!IsSquare()) throw new MException("The matrix is not square!");
        L = IdentityMatrix(rows, cols);
        U = Duplicate();

        pi = new int[rows];
        for (int i = 0; i < rows; i++) pi[i] = i;

        double p = 0;
        double pom2;
        int k0 = 0;
        int pom1 = 0;

        for (int k = 0; k < cols - 1; k++)
        {
            p = 0;
            for (int i = k; i < rows; i++)      //find the row with the biggest pivot
            {
                if (Math.Abs(U[i, k]) > p)
                {
                    p = Math.Abs(U[i, k]);
                    k0 = i;
                }
            }
            //check if singular
            if (p == 0)
                throw new MException("The matrix is singular!");

            pom1 = pi[k]; pi[k] = pi[k0]; pi[k0] = pom1;    //switch two rows in permutation matrix

            for (int i = 0; i < k; i++)
            {
                pom2 = L[k, i]; L[k, i] = L[k0, i]; L[k0, i] = pom2;
            }

            if (k != k0) detOfP *= -1;

            for (int i = 0; i < cols; i++)                  //switch rows in U
            {
                pom2 = U[k, i]; U[k, i] = U[k0, i]; U[k0, i] = pom2;
            }

            for (int i = k + 1; i < rows; i++)
            {
                L[i, k] = U[i, k] / U[k, k];
                for (int j = k; j < cols; j++)
                    U[i, j] = U[i, j] - L[i, k] * U[k, j];
            }
        }
    }

    //solves _A x = _v in confirmity with solution vector "_v"
    public Matrix SolveWith(Matrix _v)                        
    {
        if (rows != cols) throw new MException("The matrix is not square!");
        if (rows != _v.rows) throw new MException("Wrong number of results in solution vector!");
        if (L == null) MakeLU();

        Matrix b = new Matrix(rows, 1);
        for (int i = 0; i < rows; i++) b[i, 0] = _v[pi[i], 0];

        Matrix z = SubsForth(L, b);
        Matrix x = SubsBack(U, z);

        return x;
    }

    public Matrix Invert()
    {
        if (L == null) MakeLU();

        Matrix inv = new Matrix(rows, cols);

        for (int i = 0; i < rows; i++)
        {
            Matrix Ei = Matrix.ZeroMatrix(rows, 1);
            Ei[i, 0] = 1;
            Matrix col = SolveWith(Ei);
            inv.SetCol(col, i);
        }
        return inv;
    }

    public double Det()
    {
        if (L == null) MakeLU();
        double det = detOfP;
        for (int i = 0; i < rows; i++) det *= U[i, i];
        return det;
    }
    
    //return permutation matrix P
    public Matrix GetP()
    {
        if (L == null) MakeLU();

        Matrix matrix = ZeroMatrix(rows, cols);
        for (int i = 0; i < rows; i++) matrix[pi[i], i] = 1;
        return matrix;
    }

    //return copy of THIS matrix
    public Matrix Duplicate()
    {
        Matrix matrix = new Matrix(rows, cols);
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                matrix[i, j] = mat[i, j];
        return matrix;
    }
    //HELPER, solves _A x = _b for _a as a lower triangular matrix
    public static Matrix SubsForth(Matrix _a, Matrix _b)
    {
        if (_a.L == null) _a.MakeLU();
        int n = _a.rows;
        Matrix x = new Matrix(n, 1);

        for (int i = 0; i < n; i++)
        {
            x[i, 0] = _b[i, 0];
            for (int j = 0; j < i; j++) x[i, 0] -= _a[i, j] * x[j, 0];
            x[i, 0] = x[i, 0] / _a[i, i];
        }
        return x;
    }
    //HELPER, solves _A x = _b for _a as an upper triangular matrix
    public static Matrix SubsBack(Matrix _a, Matrix _b)           
    {
        if (_a.L == null) _a.MakeLU();
        int n = _a.rows;
        Matrix x = new Matrix(n, 1);

        for (int i = n - 1; i > -1; i--)
        {
            x[i, 0] = _b[i, 0];
            for (int j = n - 1; j > i; j--) x[i, 0] -= _a[i, j] * x[j, 0];
            x[i, 0] = x[i, 0] / _a[i, i];
        }
        return x;
    }

    public static Matrix ZeroMatrix(int _rows, int _cols)       // Function generates the zero matrix
    {
        Matrix matrix = new Matrix(_rows, _cols);
        for (int i = 0; i < _rows; i++)
            for (int j = 0; j < _cols; j++)
                matrix[i, j] = 0;
        return matrix;
    }

    public static Matrix IdentityMatrix(int _rows, int _cols)   // Function generates the identity matrix
    {
        Matrix matrix = ZeroMatrix(_rows, _cols);
        for (int i = 0; i < Math.Min(_rows, _cols); i++)
            matrix[i, i] = 1;
        return matrix;
    }

    public static Matrix RandomMatrix(int _rows, int _cols, int _dispersion)       // Function generates the random matrix
    {
        Random random = new Random();
        Matrix matrix = new Matrix(_rows, _cols);
        for (int i = 0; i < _rows; i++)
            for (int j = 0; j < _cols; j++)
                matrix[i, j] = random.Next(-_dispersion, _dispersion);
        return matrix;
    }

    //Add TransformMatrix maker. Rotation, Translation, Skew
    public static Matrix TransformMatrix(int _dimensions)
    {
        Matrix transform = new Matrix(_dimensions+1, _dimensions+1);
        return transform;
    }
    
    //parse the matrix from string
    public static Matrix Parse(string _ps)                        
    {
        string s = NormalizeMatrixString(_ps);
        string[] rows = Regex.Split(s, "\r\n");
        string[] nums = rows[0].Split(' ');
        Matrix matrix = new Matrix(rows.Length, nums.Length);
        try
        {
            for (int i = 0; i < rows.Length; i++)
            {
                nums = rows[i].Split(' ');
                for (int j = 0; j < nums.Length; j++) matrix[i, j] = double.Parse(nums[j]);
            }
        }
        catch (FormatException exc) { throw new MException("Wrong input format!"); }
        return matrix;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++) s += String.Format("{0,5:0.00}", mat[i, j]) + " ";
            s += "\r\n";
        }
        return s;
    }

    public static Matrix Transpose(Matrix _m)              // Matrix transpose, for any rectangular matrix
    {
        Matrix t = new Matrix(_m.cols, _m.rows);
        for (int i = 0; i < _m.rows; i++)
            for (int j = 0; j < _m.cols; j++)
                t[j, i] = _m[i, j];
        return t;
    }

    public static Matrix Power(Matrix _m, int _pow)           // Power matrix to exponent
    {
        if (_pow == 0) return IdentityMatrix(_m.rows, _m.cols);
        if (_pow == 1) return _m.Duplicate();
        if (_pow == -1) return _m.Invert();

        Matrix x;
        if (_pow < 0) { x = _m.Invert(); _pow *= -1; }
        else x = _m.Duplicate();

        Matrix ret = IdentityMatrix(_m.rows, _m.cols);
        while (_pow != 0)
        {
            if ((_pow & 1) == 1) ret *= x;
            x *= x;
            _pow >>= 1;
        }
        return ret;
    }

    private static void SafeAplusBintoC(Matrix _a, int _xa, int _ya, Matrix _b, int _xb, int _yb, Matrix _c, int _size)
    {
        for (int i = 0; i < _size; i++)          // rows
            for (int j = 0; j < _size; j++)     // cols
            {
                _c[i, j] = 0;
                if (_xa + j < _a.cols && _ya + i < _a.rows) _c[i, j] += _a[_ya + i, _xa + j];
                if (_xb + j < _b.cols && _yb + i < _b.rows) _c[i, j] += _b[_yb + i, _xb + j];
            }
    }

    private static void SafeAminusBintoC(Matrix _a, int _xa, int _ya, Matrix _b, int _xb, int _yb, Matrix _c, int _size)
    {
        for (int i = 0; i < _size; i++)          // rows
            for (int j = 0; j < _size; j++)     // cols
            {
                _c[i, j] = 0;
                if (_xa + j < _a.cols && _ya + i < _a.rows) _c[i, j] += _a[_ya + i, _xa + j];
                if (_xb + j < _b.cols && _yb + i < _b.rows) _c[i, j] -= _b[_yb + i, _xb + j];
            }
    }

    private static void SafeACopytoC(Matrix _a, int _xa, int _ya, Matrix _c, int _size)
    {
        for (int i = 0; i < _size; i++)          // rows
            for (int j = 0; j < _size; j++)     // cols
            {
                _c[i, j] = 0;
                if (_xa + j < _a.cols && _ya + i < _a.rows) _c[i, j] += _a[_ya + i, _xa + j];
            }
    }

    private static void AplusBintoC(Matrix _a, int _xa, int _ya, Matrix _b, int _xb, int _yb, Matrix _c, int _size)
    {
        for (int i = 0; i < _size; i++)          // rows
            for (int j = 0; j < _size; j++) _c[i, j] = _a[_ya + i, _xa + j] + _b[_yb + i, _xb + j];
    }

    private static void AminusBintoC(Matrix _a, int _xa, int _ya, Matrix _b, int _xb, int _yb, Matrix _c, int _size)
    {
        for (int i = 0; i < _size; i++)          // rows
            for (int j = 0; j < _size; j++) _c[i, j] = _a[_ya + i, _xa + j] - _b[_yb + i, _xb + j];
    }

    private static void ACopytoC(Matrix _a, int _xa, int _ya, Matrix _c, int _size)
    {
        for (int i = 0; i < _size; i++)          // rows
            for (int j = 0; j < _size; j++) _c[i, j] = _a[_ya + i, _xa + j];
    }

    private static Matrix StrassenMultiply(Matrix _a, Matrix _b)                // Smart matrix multiplication
    {
        if (_a.cols != _b.rows) throw new MException("Wrong dimension of matrix!");

        Matrix R;

        int msize = Math.Max(Math.Max(_a.rows, _a.cols), Math.Max(_b.rows, _b.cols));

        if (msize < 32)
        {
            R = ZeroMatrix(_a.rows, _b.cols);
            for (int i = 0; i < R.rows; i++)
                for (int j = 0; j < R.cols; j++)
                    for (int k = 0; k < _a.cols; k++)
                        R[i, j] += _a[i, k] * _b[k, j];
            return R;
        }

        int size = 1; int n = 0;
        while (msize > size) { size *= 2; n++; };
        int h = size / 2;


        Matrix[,] mField = new Matrix[n, 9];

        /*
         *  8x8, 8x8, 8x8, ...
         *  4x4, 4x4, 4x4, ...
         *  2x2, 2x2, 2x2, ...
         *  . . .
         */

        int z;
        for (int i = 0; i < n - 4; i++)          // rows
        {
            z = (int)Math.Pow(2, n - i - 1);
            for (int j = 0; j < 9; j++) mField[i, j] = new Matrix(z, z);
        }

        SafeAplusBintoC(_a, 0, 0, _a, h, h, mField[0, 0], h);
        SafeAplusBintoC(_b, 0, 0, _b, h, h, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 1], 1, mField); // (A11 + A22) * (B11 + B22);

        SafeAplusBintoC(_a, 0, h, _a, h, h, mField[0, 0], h);
        SafeACopytoC(_b, 0, 0, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 2], 1, mField); // (A21 + A22) * B11;

        SafeACopytoC(_a, 0, 0, mField[0, 0], h);
        SafeAminusBintoC(_b, h, 0, _b, h, h, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 3], 1, mField); //A11 * (B12 - B22);

        SafeACopytoC(_a, h, h, mField[0, 0], h);
        SafeAminusBintoC(_b, 0, h, _b, 0, 0, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 4], 1, mField); //A22 * (B21 - B11);

        SafeAplusBintoC(_a, 0, 0, _a, h, 0, mField[0, 0], h);
        SafeACopytoC(_b, h, h, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 5], 1, mField); //(A11 + A12) * B22;

        SafeAminusBintoC(_a, 0, h, _a, 0, 0, mField[0, 0], h);
        SafeAplusBintoC(_b, 0, 0, _b, h, 0, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 6], 1, mField); //(A21 - A11) * (B11 + B12);

        SafeAminusBintoC(_a, h, 0, _a, h, h, mField[0, 0], h);
        SafeAplusBintoC(_b, 0, h, _b, h, h, mField[0, 1], h);
        StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 7], 1, mField); // (A12 - A22) * (B21 + B22);

        R = new Matrix(_a.rows, _b.cols);                  // result

        /// C11
        for (int i = 0; i < Math.Min(h, R.rows); i++)          // rows
            for (int j = 0; j < Math.Min(h, R.cols); j++)     // cols
                R[i, j] = mField[0, 1 + 1][i, j] + mField[0, 1 + 4][i, j] - mField[0, 1 + 5][i, j] + mField[0, 1 + 7][i, j];

        /// C12
        for (int i = 0; i < Math.Min(h, R.rows); i++)          // rows
            for (int j = h; j < Math.Min(2 * h, R.cols); j++)     // cols
                R[i, j] = mField[0, 1 + 3][i, j - h] + mField[0, 1 + 5][i, j - h];

        /// C21
        for (int i = h; i < Math.Min(2 * h, R.rows); i++)          // rows
            for (int j = 0; j < Math.Min(h, R.cols); j++)     // cols
                R[i, j] = mField[0, 1 + 2][i - h, j] + mField[0, 1 + 4][i - h, j];

        /// C22
        for (int i = h; i < Math.Min(2 * h, R.rows); i++)          // rows
            for (int j = h; j < Math.Min(2 * h, R.cols); j++)     // cols
                R[i, j] = mField[0, 1 + 1][i - h, j - h] - mField[0, 1 + 2][i - h, j - h] + mField[0, 1 + 3][i - h, j - h] + mField[0, 1 + 6][i - h, j - h];

        return R;
    }

    // function for square matrix 2^N x 2^N

    private static void StrassenMultiplyRun(Matrix _a, Matrix _b, Matrix _c, int _l, Matrix[,] _f)    // _a * _b into _c, level of recursion, matrix field
    {
        int size = _a.rows;
        int h = size / 2;

        if (size < 32)
        {
            for (int i = 0; i < _c.rows; i++)
                for (int j = 0; j < _c.cols; j++)
                {
                    _c[i, j] = 0;
                    for (int k = 0; k < _a.cols; k++) _c[i, j] += _a[i, k] * _b[k, j];
                }
            return;
        }

        AplusBintoC(_a, 0, 0, _a, h, h, _f[_l, 0], h);
        AplusBintoC(_b, 0, 0, _b, h, h, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 1], _l + 1, _f); // (A11 + A22) * (B11 + B22);

        AplusBintoC(_a, 0, h, _a, h, h, _f[_l, 0], h);
        ACopytoC(_b, 0, 0, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 2], _l + 1, _f); // (A21 + A22) * B11;

        ACopytoC(_a, 0, 0, _f[_l, 0], h);
        AminusBintoC(_b, h, 0, _b, h, h, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 3], _l + 1, _f); //A11 * (B12 - B22);

        ACopytoC(_a, h, h, _f[_l, 0], h);
        AminusBintoC(_b, 0, h, _b, 0, 0, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 4], _l + 1, _f); //A22 * (B21 - B11);

        AplusBintoC(_a, 0, 0, _a, h, 0, _f[_l, 0], h);
        ACopytoC(_b, h, h, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 5], _l + 1, _f); //(A11 + A12) * B22;

        AminusBintoC(_a, 0, h, _a, 0, 0, _f[_l, 0], h);
        AplusBintoC(_b, 0, 0, _b, h, 0, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 6], _l + 1, _f); //(A21 - A11) * (B11 + B12);

        AminusBintoC(_a, h, 0, _a, h, h, _f[_l, 0], h);
        AplusBintoC(_b, 0, h, _b, h, h, _f[_l, 1], h);
        StrassenMultiplyRun(_f[_l, 0], _f[_l, 1], _f[_l, 1 + 7], _l + 1, _f); // (A12 - A22) * (B21 + B22);

        /// C11
        for (int i = 0; i < h; i++)          // rows
            for (int j = 0; j < h; j++)     // cols
                _c[i, j] = _f[_l, 1 + 1][i, j] + _f[_l, 1 + 4][i, j] - _f[_l, 1 + 5][i, j] + _f[_l, 1 + 7][i, j];

        /// C12
        for (int i = 0; i < h; i++)          // rows
            for (int j = h; j < size; j++)     // cols
                _c[i, j] = _f[_l, 1 + 3][i, j - h] + _f[_l, 1 + 5][i, j - h];

        /// C21
        for (int i = h; i < size; i++)          // rows
            for (int j = 0; j < h; j++)     // cols
                _c[i, j] = _f[_l, 1 + 2][i - h, j] + _f[_l, 1 + 4][i - h, j];

        /// C22
        for (int i = h; i < size; i++)          // rows
            for (int j = h; j < size; j++)     // cols
                _c[i, j] = _f[_l, 1 + 1][i - h, j - h] - _f[_l, 1 + 2][i - h, j - h] + _f[_l, 1 + 3][i - h, j - h] + _f[_l, 1 + 6][i - h, j - h];
    }

    public static Matrix StupidMultiply(Matrix _m1, Matrix _m2)
    {
        if (_m1.cols != _m2.rows) throw new MException("Wrong dimensions of matrix!");

        Matrix result = ZeroMatrix(_m1.rows, _m2.cols);
        for (int i = 0; i < result.rows; i++)
            for (int j = 0; j < result.cols; j++)
                for (int k = 0; k < _m1.cols; k++)
                    result[i, j] += _m1[i, k] * _m2[k, j];
        return result;
    }
    private static Matrix Multiply(double _n, Matrix _m)
    {
        Matrix r = new Matrix(_m.rows, _m.cols);
        for (int i = 0; i < _m.rows; i++)
            for (int j = 0; j < _m.cols; j++)
                r[i, j] = _m[i, j] * _n;
        return r;
    }

    private static Matrix Add(Matrix _m1, Matrix _m2)
    {
        if (_m1.rows != _m2.rows || _m1.cols != _m2.cols) throw new MException("Matrices must have the same dimensions!");
        Matrix r = new Matrix(_m1.rows, _m1.cols);
        for (int i = 0; i < r.rows; i++)
            for (int j = 0; j < r.cols; j++)
                r[i, j] = _m1[i, j] + _m2[i, j];
        return r;
    }

    public static string NormalizeMatrixString(string _matStr)
    {
        // Remove any multiple spaces
        while (_matStr.IndexOf("  ") != -1)
            _matStr = _matStr.Replace("  ", " ");

        // Remove any spaces before or after newlines
        _matStr = _matStr.Replace(" \r\n", "\r\n");
        _matStr = _matStr.Replace("\r\n ", "\r\n");

        // If the data ends in a newline, remove the trailing newline.
        // Make it easier by first replacing \r\n’s with |’s then
        // restore the |’s with \r\n’s
        _matStr = _matStr.Replace("\r\n", "|");
        while (_matStr.LastIndexOf("|") == (_matStr.Length - 1))
            _matStr = _matStr.Substring(0, _matStr.Length - 1);

        _matStr = _matStr.Replace("|", "\r\n");
        return _matStr.Trim();
    }

    //OPERATORS

    public static Matrix operator -(Matrix _m)
    { return Matrix.Multiply(-1, _m); }

    public static Matrix operator +(Matrix _m1, Matrix _m2)
    { return Matrix.Add(_m1, _m2); }

    public static Matrix operator -(Matrix _m1, Matrix _m2)
    { return Matrix.Add(_m1, -_m2); }

    public static Matrix operator *(Matrix _m1, Matrix _m2)
    { return Matrix.StrassenMultiply(_m1, _m2); }

    public static Matrix operator *(double n, Matrix _m)
    { return Matrix.Multiply(n, _m); }
}

//class for exceptions

public class MException : Exception
{
    public MException(string _Message)
        : base(_Message)
    { }
}
