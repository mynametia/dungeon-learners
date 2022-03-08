public class Question
{
    private string question;
    private string[] options;
    private int answer; // index of options that holds correct answer

    public Question(string Q, string[] op, int A)
    {
        question = Q;
        answer = A;
        options = op;
    }
}
