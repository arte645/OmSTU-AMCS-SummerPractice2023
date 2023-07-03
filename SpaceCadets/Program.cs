using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpaceCadets;
public class Program
{
    public class Student
    {
        public string Name = "";
        public string Group = "";
        public string Discipline = "";
        public double Mark = 0;
    }

    public static void Main(string[] args)
    {
        string input_path = args[0];
        string output_path = args[1];

        string file = File.ReadAllText(input_path);

        dynamic json_file = JsonConvert.DeserializeObject(file) ?? new JObject();
        List<Student> students = json_file.data.ToObject<List<Student>>();
        string scenario = json_file.taskName;
        List<dynamic> result;

        switch (scenario)
        {
            case "GetStudentsWithHighestGPA":
                {
                    result = GetStudentsWithHighestGPA(students);
                    break;
                }
            case "CalculateGPAByDiscipline":
                {
                    result = CalculateGPAByDiscipline(students);
                    break;
                }
            case "GetBestGroupsByDiscipline":
                {
                    result = GetBestGroupsByDiscipline(students);
                    break;
                }
            default:
                throw new Exception();
        }
        var json = new {Response = result};
        string exit_file = JsonConvert.SerializeObject(json, Formatting.Indented);

        File.WriteAllText(output_path, exit_file);
    }

    public static List<dynamic> GetStudentsWithHighestGPA(List<Student> students)
    {
        var students_with_all_marks = students.GroupBy(p => p.Name).Select(g => new
        {
            Name = g.Key,
            Marks = g.Select(p => p.Mark).ToArray()
        });

        double HighestGpa = students_with_all_marks.Max(student => (student.Marks[0] + student.Marks[1]) / 2);
        var result = students_with_all_marks.Where(student => (student.Marks[0] + student.Marks[1]) / 2 == HighestGpa).Select(student => new
        {
            Name = student.Name,
            Mark = (int)((student.Marks[0] + student.Marks[1]) / 2)
        });
        List<dynamic> exit_list = result.ToList<dynamic>();
        return exit_list;
    }

    public static List<dynamic> CalculateGPAByDiscipline(List<Student> students)
    {
        var disciplines_with_all_marks = students.GroupBy(p => p.Discipline).Select(g => new
        {
            Discipline = g.Key,
            Marks = g.Select(p => p.Mark).ToArray()
        });

        var result = disciplines_with_all_marks.Select(p => new
        {
            Discipline = p.Discipline,
            Mark = Math.Round(p.Marks.Sum() / p.Marks.Length, 2)
        });

        List<dynamic> exit_list = result.ToList<dynamic>();
        return exit_list;
    }

    public static List<dynamic> GetBestGroupsByDiscipline(List<Student> students)
    {
        var groups_with_all_marks = students.GroupBy(p => new { Discipline = p.Discipline, Group = p.Group }).Select(g => new
        {
            Discipline = g.Key.Discipline,
            Group = g.Key.Group,
            Mark = g.Average(v => v.Mark)}).GroupBy(p => p.Discipline)
                            .Select(q => new{
                                Discipline = q.Key,
                                Group = q.Where(k => k.Mark == q.Max(z => z.Mark)).Select(k => k.Group).ToArray()[0],
                                GPA = q.Max(k => k.Mark)
                            }).ToList<dynamic>();
        return groups_with_all_marks;
    }
}
