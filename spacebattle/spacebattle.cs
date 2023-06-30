namespace spacebattle;
public class Spacebattle
{
    public static double[] FindPosition(double[] position, double[] speed, bool can_move)
    {
        var result = new double[2];
        if(can_move)
        {
            try
            {
                result[0] = position[0] + speed[0];
                result[1] = position[1] + speed[1];
            }   
            catch
            {
                throw new System.Exception();
            }
        }
        else throw new System.Exception();
        return result; 
    }
}
