using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Animals : MonoBehaviour
{
    public Transform letterLocation;
    public GameObject letterWordPrefab;
    public Transform letterWordParent;
    public GameObject letterBoxPrefab;
    public Transform letterBoxParent;
    public string wordToGuess;
    private bool gameStarted = false;
    public Text startText;
    public Text[] letterWords;
    public int currentIndex = 0;
    public float spacing = 5f;
    public int guesses;
    public int maxAttempts;
    public string lastGuess; //Gjord f�r att visa sista gissningen fr�n spelaren.
    public int randomIndex;
    public string hint;
    public Text hintText;
    public GameObject hintPopup;
    public Hearts heart;
    public Timer timer;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        timer.SetDifficulty();
        FillBeginnerMoviesSeries(); //Det h�r �ndrades
        FillIntermediateMoviesSeries();//Det h�r �ndrades
        FillAdvancedMoviesSeries();//Det h�r �ndrades

        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Cat3 Beginner"://Det h�r �ndrades
                wordToGuess = GetRandomName(beginnerAnimals);//Det h�r �ndrades
                SetLetterBoxes();
                SetLetterWords(wordToGuess.Length);
                hint = GetHint(beginnerAnimals);//Det h�r �ndrades
                break;
            case "Cat3 Intermediate"://Det h�r �ndrades
                wordToGuess = GetRandomName(intermediateAnimals);//Det h�r �ndrades
                SetLetterBoxes();
                SetLetterWords(wordToGuess.Length);
                hint = GetHint(intermediateAnimals);//Det h�r �ndrades
                break;
            case "Cat3 Advanced"://Det h�r �ndrades
                wordToGuess = GetRandomName(advancedAnimals);//Det h�r �ndrades
                SetLetterBoxes();
                SetLetterWords(wordToGuess.Length);
                hint = GetHint(advancedAnimals);//Det h�r �ndrades
                break;
        }

        Debug.Log("Word to Guess: " + wordToGuess);
        Debug.Log("Enter your Guess:");

    } //Checka if n�gra calls beh�ver �ndras eller bytas ut

    void Update()
    {
        ReadPlayerInput();
    }//Checka if n�gra calls beh�ver �ndras eller bytas ut

    public class Animal
    {
        public string name;
        public string difficulty;
        public string hint;

        public Animal(string name, string difficulty, string hint)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.hint = hint;
        }
    } //�ndra endast namnet p� detta s� det passar rummet

    private List<Animal> beginnerAnimals = new List<Animal>();
    private List<Animal> intermediateAnimals = new List<Animal>();
    private List<Animal> advancedAnimals = new List<Animal>();

    private void FillBeginnerMoviesSeries()
    {
        beginnerAnimals.Add(new Animal("Bear", "Beginner", "Big furry, only friendly if russian"));
        beginnerAnimals.Add(new Animal("Duck", "Beginner", "Quack quack"));
        beginnerAnimals.Add(new Animal("Panda", "Beginner", "Dumbest bears ever"));

    } //�ndra f�r specifik script //Det h�r �ndrades

    private void FillIntermediateMoviesSeries()
    {
        intermediateAnimals.Add(new Animal("Otter", "Intermediate", "Playful aquatic mammal"));
        intermediateAnimals.Add(new Animal("Koala", "Intermediate", "Care for piss, should be extinct by now"));
        intermediateAnimals.Add(new Animal("Turtle", "Intermediate", "Pretty slow, watch out for snaps"));

    } //�ndra f�r specifik script //Det h�r �ndrades

    private void FillAdvancedMoviesSeries()
    {
        advancedAnimals.Add(new Animal("Beaver", "Advanced", "Creates great walls, crazy front teeth"));
        advancedAnimals.Add(new Animal("Jaguar", "Advanced", "Second fastest feline"));
        advancedAnimals.Add(new Animal("Octopus", "Advanced", "Lives in water with too many arms"));

    } //�ndra f�r specifik script //Det h�r �ndrades

    public List<Animal> GetCharacters(string difficulty) //Denna kod beh�ver �ndras f�r att passa scenen som spelas och taggen som blir vald. 
    {
        switch (difficulty)
        {
            case "MSBeginner"://Det h�r �ndrades
                return beginnerAnimals;//Det h�r �ndrades
            case "MSIntermediate"://Det h�r �ndrades
                return intermediateAnimals;//Det h�r �ndrades
            case "MSAdvanced"://Det h�r �ndrades
                return advancedAnimals;//Det h�r �ndrades
            default:
                return null;
        }
    }

    public void SetLetterBoxes() //Det h�r S�tter fram den synliga basen f�r alla bokst�ver, �NDRA EJ
    {
        if (string.IsNullOrEmpty(wordToGuess))
        {
            Debug.LogError("Word to guess is null or empty!");
            return;
        }


        float totalWidth = wordToGuess.Length * 2f;


        float startX = 2f - totalWidth / 2f + 2f;

        for (int i = 0; i < wordToGuess.Length; i++)
        {
            GameObject letterBox = Instantiate(letterBoxPrefab, letterBoxParent);


            letterBox.transform.localPosition = new Vector3(startX + i * 2f, 0f, 0f);

            letterBox.name = "Letterbox." + i;
        }
    }

    private string GetRandomName(List<Animal> characters)//Det h�r �ndrades
    {
        if (characters.Count == 0)
        {
            Debug.LogError("List is empty!");
            return "";
        }

        randomIndex = Random.Range(0, characters.Count);
        return characters[randomIndex].name;
    } //Det h�r drar olika ord varje g�ng det spelas, �NDRA EJ (f�rutom vilken slags lista)

    void ReadPlayerInput()  //L�ser in spelaren input (Vad den skriver), denna kod beh�ver EJ �NDRAS den passar i alla scripts. Dock kan kopplingen med timer.SetDifficulty() �ndras f�r att passa annan script.
    {


        if (!gameStarted)
        {
            startText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                startText.gameObject.SetActive(false);
                gameStarted = true;
            }

        }
        else
        {
            string playerInput = Input.inputString;
            if (!string.IsNullOrEmpty(playerInput))
            {
                Debug.Log("Player input: " + playerInput);


                if (currentIndex < letterWords.Length)
                {
                    letterWords[currentIndex].text = playerInput;
                    currentIndex++;
                }
                if (currentIndex >= letterWords.Length)
                {
                    maxAttempts = timer.SetDifficulty();
                    Debug.Log("Fictional Max Attempts are: " + maxAttempts);
                    Debug.Log("Attempt has been made");
                    AttemptGuess(maxAttempts);
                }
            }
        }
    }

    private void AttemptGuess(int chances) //Begr�nsar antal f�rs�k spelaren har och �ven s�tter fram vinst/f�rlust utkomst.
    {
        bool guessMore = false;
        bool wonGame = false;
        bool lostGame = false;
        string guess = "";
        int hearts = maxAttempts;
        Debug.Log("Player input: " + guess);
        Debug.Log("Word to guess:" + wordToGuess);

        if (chances == 0)
        {
            Debug.Log("0 Chances");
            return;
        }

        if (guesses == chances)
        {
            Debug.Log("Sorry, your guesses ran out");
            letterBoxParent.gameObject.SetActive(false);
            lostGame = true;
            timer.theAnswer.text = wordToGuess;
            timer.yourGuess.text = lastGuess;
            timer.GameOver(wonGame, lostGame);
            return;
        }
        else
        {
            for (int i = 0; i < letterWords.Length; i++)
            {
                guess += letterWords[i].text;
            }
            lastGuess = guess;

            if (guess.ToLower() == wordToGuess.ToLower())
            {
                guessMore = true;
                Debug.Log("Word guessed: " + guess);
                Debug.Log("Congratulations! You guessed the word correctly!");
                letterBoxParent.gameObject.SetActive(false);
                wonGame = true;
                timer.theAnswer.text = wordToGuess; //visar svaret i en annan script.
                timer.yourGuess.text = lastGuess; //Visar sista gissningen i en annan script.
                timer.GameOver(wonGame, lostGame);
            }
            else
            {
                Debug.Log("Sorry, that's not the correct word.");
                Debug.Log("Guess: " + guess);
                guesses++;
                heart.RemoveHeart();
            }

            if (!guessMore && guesses < chances)
            {
                Debug.Log("You have " + (chances - guesses) + " guesses left.");
                Debug.Log("Please enter another guess.");

            }

            if (!guessMore)
            {
                currentIndex = 0;
                foreach (Text letterword in letterWords)
                {
                    letterword.text = "";
                }
            }
        }
    }

    void SetLetterWords(int numberOfLetters) //S�tter fram spelarens input f�r synlighetsskull �NDRA EJ
    {
        if (string.IsNullOrEmpty(wordToGuess))
        {
            Debug.LogError("Word to guess is null or empty!");
            return;
        }


        float totalWidth = numberOfLetters * (letterWordPrefab.GetComponent<RectTransform>().rect.width * 1.8f + spacing);


        float startX = 2f - totalWidth / 2.5f;

        float xOffset = startX + 200f;
        float yOffset = 1f + 45f;

        letterWords = new Text[numberOfLetters];
        spacing = spacing + 2;
        for (int i = 0; i < numberOfLetters; i++)
        {
            GameObject letterWord = Instantiate(letterWordPrefab, letterWordParent);


            RectTransform rectTransform = letterWord.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);

            letterWords[i] = letterWord.GetComponent<Text>();
            letterWord.name = "Letterword." + i;

            xOffset += rectTransform.rect.width + spacing;
        }
    }

    public string GetHint(List<Animal> hint)//Det h�r �ndrades //D� click delarna �r en del av categories, referera till den inom denna metod. Inom denna ska det finnas setActive f�r popupen samt texten
    { //Eller g�r en seperat script bara f�r hint knappen d� det inte trasslas med andra delar.
        if (hint == null)
        {
            Debug.LogError("there is no hint!");
            return "";
        }

        hintText.text = hint[randomIndex].hint;
        return hintText.text;
    }
}
