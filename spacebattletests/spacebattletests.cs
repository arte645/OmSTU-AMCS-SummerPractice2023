using spacebattle;
using TechTalk.SpecFlow;
namespace spacebattletests
{
    [Binding]
    public class Spacebattletests
    {
        private ScenarioContext scenarioContext;
        private double[] position = new double[2];
        private double[] speed= new double[2];
        public bool can_move = true;
        public double[] result = new double[2];
        public Spacebattletests(ScenarioContext input)
        {
            scenarioContext = input;
        }
        [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
         public void Input_with_real_numbers(double koef1, double koef2)
         {
            position = new double[2];

             position[0] = koef1;
             position[1] = koef2;
         }
         [Given(@"космический корабль, положение в пространстве которого невозможно определить")]
         public void Input_with_firstNan()
         {
            position = new double[0];
         }
         [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
         public void Input_speed(double koef1, double koef2)
         {
            speed = new double[2];

            speed[0] = koef1;
            speed[1] = koef2;
         }
         [Given(@"скорость корабля определить невозможно")]
         public void Input_wrong_speed()
         {
             speed = new double[0];
         }
         [Given(@"изменить положение в пространстве космического корабля невозможно")]
         public void Input_ban_moving()
         {
            can_move = false;
         }
         
        [When(@"происходит прямолинейное равномерное движение без деформации")]
        public void try_to_solve()
        {
            try{
                result = Spacebattle.FindPosition(position, speed,can_move);
            }
            catch{
            }
        }

       [Then(@"космический корабль перемещается в точку пространства с координатами \((.*), (.*)\)")]
        public void Test_for_normal_position(double koef3, double koef4)
        {
            double[] excepted = new double[] {koef3, koef4};

            for(int i = 0;i<result.Length;i++)
                Assert.Equal(excepted[i], result[i]);
        }

        [Then(@"возникает ошибка Exception")]
         public void Test_for_get_stuck_in_textures()
         {
            Assert.Throws<Exception>(() => Spacebattle.FindPosition(position, speed,can_move));
         }
    }
}