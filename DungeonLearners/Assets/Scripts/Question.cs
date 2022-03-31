/// <summary>
/// Question class
/// </summary>
public class Question
{
    public string question;
    public string[] options;
    public int answer; // index of options that holds correct answer

    public Question(string Q, string[] op, int A)
    {
        question = Q;
        answer = A;
        options = op;
    }
}
