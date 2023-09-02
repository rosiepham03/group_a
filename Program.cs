using System;

public class Question
{
    public string Text { get; set; }
    public List<string> Choices { get; set; }
    public int CorrectChoiceIndex { get; set; }

    public bool IsAnswerCorrect(int choiceIndex)
    {
        return choiceIndex == CorrectChoiceIndex;
    }

    // Thêm phương thức để xáo trộn các lựa chọn
    public void ShuffleChoices()
    {
        // Sử dụng thuật toán Fisher-Yates
        Random random = new Random();
        for (int i = Choices.Count - 1; i > 0; i--)
        {
            // Chọn một phần tử ngẫu nhiên từ 0 đến i
            int j = random.Next(i + 1);

            // Đổi chỗ phần tử tại vị trí i và j
            string temp = Choices[i];
            Choices[i] = Choices[j];
            Choices[j] = temp;

            // Cập nhật chỉ số của câu trả lời đúng nếu cần
            if (i == CorrectChoiceIndex)
            {
                CorrectChoiceIndex = j;
            }
            else if (j == CorrectChoiceIndex)
            {
                CorrectChoiceIndex = i;
            }
        }
    }
}

public class Quiz
{
    private List<Question> questions;
    private int currentQuestionIndex;

    public Quiz(List<Question> quizQuestions)
    {
        questions = quizQuestions;
        currentQuestionIndex = 0;
    }

    public Question GetCurrentQuestion()
    {
        return questions[currentQuestionIndex];
    }

    public bool MoveToNextQuestion()
    {
        if (currentQuestionIndex < questions.Count - 1)
        {
            currentQuestionIndex++;
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Yêu cầu người dùng nhập tên của họ
        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();
        // Tạo danh sách câu hỏi
        List<Question> questions = new List<Question>
        {
            new Question
            {
                Text = "What is the capital of France?",
                Choices = new List<string> { "Paris", "London", "Berlin", "Madrid" },
                CorrectChoiceIndex = 0
            },
            new Question
            {
                Text = "Who painted the Mona Lisa?",
                Choices = new List<string> { "Leonardo da Vinci", "Pablo Picasso", "Vincent van Gogh", "Michelangelo" },
                CorrectChoiceIndex = 0
            },
            new Question
            {
                Text = "What is the largest animal in the world?",
                Choices = new List<string> { "Elephant", "Whale", "Giraffe", "Dinosaur" },
                CorrectChoiceIndex = 1
            },
            // Thêm câu hỏi khác vào đây...
        };

        // Tạo đối tượng Quiz
        Quiz quiz = new Quiz(questions);

        // Tạo biến để lưu trữ số câu trả lời đúng
        int score = 0;

        // Lặp qua các câu hỏi trong danh sách
        foreach (Question question in questions)
        {
            // Hiển thị câu hỏi hiện tại
            Console.WriteLine(question.Text);

            // Xáo trộn các lựa chọn của câu hỏi hiện tại
            question.ShuffleChoices();

            for (int i = 0; i < question.Choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Choices[i]}");
            }

            // Nhận câu trả lời từ người dùng
            Console.Write("Your answer: ");
            int userAnswer = int.Parse(Console.ReadLine());

            // Kiểm tra câu trả lời có đúng không
            if (question.IsAnswerCorrect(userAnswer - 1))
            {
                Console.WriteLine("Correct!");
                // Tăng điểm của người dùng
                score++;
            }
            else
            {
                Console.WriteLine("Wrong!");
            }
        }


        // Hiển thị điểm của người dùng
        Console.WriteLine($"Your score is {score} out of {questions.Count}, {userName}");
    }
}
