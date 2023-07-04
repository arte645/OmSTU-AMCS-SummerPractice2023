namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        double[] solution;
        double e = 1e-6;    

        if ((a < e && a > -e) || double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c)
            || double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c))
        {
            throw new System.ArgumentException();
        }

        if (Math.Abs(b)>e)
        {
            b = b / a;
            c = c / a;

            double discriminant = Math.Pow(b,2) - 4 * c ;

            if (discriminant <= -e)
            {
                solution = new double[0];
            }
            else if (discriminant < e && discriminant>-e)
            {
                solution = new double[1];
                solution[0]= -b / 2;
            }
            else
            {
                solution = new double[2];
                solution[0] = -(b + Math.Sign(b) * Math.Sqrt(discriminant)) / 2;
                solution[1] = c / solution[0];
            }
        }
        else if (c<=e)
        {
            solution = new double[2];
            solution[0] = Math.Pow(Math.Abs(c),0.5);
            solution[1] = -Math.Pow(Math.Abs(c),0.5);
        }
        else solution= new double[0];

        return solution;
    }
}