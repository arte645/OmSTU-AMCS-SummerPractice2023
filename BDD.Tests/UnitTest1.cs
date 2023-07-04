using SquareEquationLib;
using TechTalk.SpecFlow;
namespace UnitTes1
{
    [Binding]
    public class BDD
    {
        private ScenarioContext scenarioContext;
        private double a = 0;
        private double b = 0;
        private double c = 0;
        public double[] result = new double[2];
        public BDD(ScenarioContext input)
        {
            scenarioContext = input;
        }
        [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
         public void Input_with_real_numbers(double koef1, double koef2, double koef3)
         {
             a = koef1;
             b = koef2;
             c = koef3;
         }

         [Given(@"Квадратное уравнение с коэффициентами \(NaN, (.*), (.*)\)")]
         public void Input_with_firstNan(double koef1, double koef2)
         {
             a = double.NaN;
             b = koef1;
             c = koef2;
         }
         [Given(@"Квадратное уравнение с коэффициентами \((.*), NaN, (.*)\)")]
         public void Input_with_secondNan(double koef1, double koef2)
         {
             a = koef1;
             b = double.NaN;
             c = koef2;
         }
         [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), NaN\)")]
         public void Input_with_thirdNan(double koef1, double koef2)
         {
             a = koef1;
             b = koef2;
             c = double.NaN;
         }
         [Given(@"Квадратное уравнение с коэффициентами \(Double\.NegativeInfinity, (.*), (.*)\)")]
         public void Input_with_firstNI(double koef1, double koef2)
         {
             a = double.NegativeInfinity;
             b = koef1;
             c = koef2;
         }
         [Given(@"Квадратное уравнение с коэффициентами \((.*), Double\.NegativeInfinity, (.*)\)")]
         public void Input_with_secondNI(double koef1, double koef2)
         {
             a = koef1;
             b = double.NegativeInfinity;
             c = koef2;
         }
         [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), Double\.NegativeInfinity\)")]
         public void Input_with_thirdNI(double koef1, double koef2)
         {
             a = koef1;
             b = koef2;
             c = double.NegativeInfinity;
         }
         [Given(@"Квадратное уравнение с коэффициентами \(Double\.PositiveInfinity, (.*), (.*)\)")]
         public void Input_with_firstPI(double koef1, double koef2)
         {
             a = double.PositiveInfinity;
             b = koef1;
             c = koef2;
         }
         [Given(@"Квадратное уравнение с коэффициентами \((.*), Double\.PositiveInfinity, (.*)\)")]
         public void Input_with_secondPI(double koef1, double koef2)
         {
             a = koef1;
             b = double.PositiveInfinity;
             c = koef2;
         }
         [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), Double\.PositiveInfinity\)")]
         public void Input_with_thirdPI(double koef1, double koef2)
         {
             a = koef1;
             b = koef2;
             c = double.PositiveInfinity;
         }
        [When(@"вычисляются корни квадратного уравнения")]
        public void try_to_solve()
        {
            try{
                result = SquareEquation.Solve(a,b,c);
            }
            catch{
            }
        }
       [Then(@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
        public void Test_for_two_roots(double koef1, double koef2)
        {
            double[] expected = new double[] {koef1, koef2};

            Assert.Equal(expected, result);
        }

        [Then(@"квадратное уравнение имеет один корень (.*) кратности два")]
         public void Test_for_one_root(double koef1)
         {
            double[] expected = new double[] {koef1};

            Assert.Equal(expected, result);
         }

         [Then(@"множество корней квадратного уравнения пустое")]
         public void Test_for_no_roots()
         {
            Assert.Empty(result);
         }

         [Then(@"выбрасывается исключение ArgumentException")]
         public void Test_for_Exception()
         {
            Assert.Throws<System.ArgumentException>(() => SquareEquation.Solve(a, b, c));
         }
    }
}