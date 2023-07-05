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
        private double fuel;
        private double delta_fuel;
        private double angle;
        private double delta_angle;
      
        public Spacebattletests(ScenarioContext input)
        {
            scenarioContext = input;
        }
        [Given(@"космический корабль имеет топливо в объеме (.*) ед")]
        public void Input_fuel(double koef1)
        {
            fuel=koef1;
        }


        [Given(@"космический корабль имеет угол наклона (.*) град к оси OX")]
        public void Input_with_angle_and_axis(double koef1)
        {
            angle = koef1;
        }
        [Given(@"космический корабль, угол наклона которого невозможно определить")]
        public void Input_with_Nan_angle()
        {
            angle = double.NaN;
        }
        [Given(@"имеет скорость расхода топлива при движении (.*) ед")]
        public void Input_delta_fuel(double koef1)
        {
            delta_fuel=koef1;
        }
        [Given(@"имеет мгновенную угловую скорость (.*) град")]
        public void Input_delta_Angle(double koef1)
        {
            delta_angle = koef1;
        }
        [Given(@"мгновенную угловую скорость невозможно определить")]
        public void Input_delta_AngleNaN1()
        {
            delta_angle=double.NaN;
        }
        [Given(@"невозможно изменить уголд наклона к оси OX космического корабля")]
        public void Input_delta_AngleNaN2()
        {
            delta_angle=double.NaN;
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
        public void try_to_FindFuel()
        {
            if(fuel!=double.NaN || delta_fuel!=double.NaN)
            {
                try{
                    var result = Spacebattle.FindFuel(fuel, delta_fuel);
                }
                catch{
                }
            }
            else
            {
                try{
                var result = Spacebattle.FindPosition(position, speed,can_move);
                }
                catch{
                }
            }
        }
        [When(@"происходит вращение вокруг собственной оси")]
        public void try_to_Findangle()
        {
            try{
                var result = Spacebattle.FindAngle(angle, delta_angle);
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
        [Then(@"новый объем топлива космического корабля равен (.*) ед")]
        public void Test_for_normal_fuel(double koef3)
        {
            double result = Spacebattle.FindFuel(fuel, delta_fuel);
            double excepted = koef3;

            Assert.Equal(excepted, result);
        }
        [Then(@"угол наклона космического корабля к оси OX составляет (.*) град")]
        public void Test_for_normal_angle(double koef3)
        {
            double result = Spacebattle.FindAngle(angle, delta_angle);
            double excepted = koef3;

        Assert.Equal(excepted, result);
        }

        [Then(@"возникает ошибка Exception")]
         public void Test_for_get_stuck_in_textures()
         {
            if(fuel!=double.NaN || delta_fuel!=double.NaN)
            {
              Assert.Throws<Exception>(() => Spacebattle.FindFuel(fuel, delta_fuel));
            }
            else if(angle!=double.NaN || delta_angle!=double.NaN)
            {
              Assert.Throws<Exception>(() => Spacebattle.FindAngle(angle, delta_angle));
            }
            else
            {
                Assert.Throws<Exception>(() => Spacebattle.FindPosition(position, speed,can_move));
            }
        }
    }
}