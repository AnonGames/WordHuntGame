

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Fiction : MonoBehaviour
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
    public int currentIndex = -1;
    public float spacing = 5f;
    public int guesses;
    public int maxAttempts;
    public string lastGuess; //Gjord för att visa sista gissningen från spelaren.
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
        FillBeginnerCharacters();
        FillIntermediateCharacters();
        FillAdvancedCharacters();

        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Cat1 Beginner":
                wordToGuess = GetRandomName(beginnerCharacters);
                SetLetterBoxes();
                SetLetterWords(wordToGuess.Length);
                hint = GetHint(beginnerCharacters);
                break;
            case "Cat1 Intermediate":
                wordToGuess = GetRandomName(intermediateCharacters);
                SetLetterBoxes();
                SetLetterWords(wordToGuess.Length);
                hint = GetHint(intermediateCharacters);
                break;
            case "Cat1 Advanced":
                wordToGuess = GetRandomName(advancedCharacters);
                SetLetterBoxes();
                SetLetterWords(wordToGuess.Length);
                hint = GetHint(advancedCharacters);
                break;
        }

        Debug.Log("Word to Guess: " + wordToGuess);
        Debug.Log("Enter your Guess:");

        /*string playerGuess = Console.ReadLine(); Learned that this line of code doesnt work on unity and ill need to change it to readinput instead.
        if (playerGuess.ToLower() == wordToGuess.ToLower())
        {
            Debug.Log("Congratulations! You guessed the word correctly!");
        }
        else
        {
            Debug.Log("Sorry, that's not the correct word.");
        }*/
        

    } //Checka if några calls behöver ändras eller bytas ut

    void Update()
    {
        ReadPlayerInput();
    }//Checka if några calls behöver ändras eller bytas ut

    public class FictionalCharacter
    {
        public string name;
        public string difficulty;
        public string hint;

        public FictionalCharacter(string name, string difficulty, string hint)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.hint = hint;
        }
    } //Ändra endast namnet på detta så det passar rummet

    private List<FictionalCharacter> beginnerCharacters = new List<FictionalCharacter>();
    private List<FictionalCharacter> intermediateCharacters = new List<FictionalCharacter>();
    private List<FictionalCharacter> advancedCharacters = new List<FictionalCharacter>();

    private void FillBeginnerCharacters()
    {
        beginnerCharacters.Add(new FictionalCharacter("Finn", "Beginner", "Adventure Time, Cartoon"));
        beginnerCharacters.Add(new FictionalCharacter("Asta", "Beginner", "Black Clover, Anime"));
        beginnerCharacters.Add(new FictionalCharacter("Zelda", "Beginner", "Nintendo, Famous game"));

    } //Ändra för specifik script

    private void FillIntermediateCharacters()
    {
        intermediateCharacters.Add(new FictionalCharacter("Shrek", "Intermediate", "Famous Pixar movie"));
        intermediateCharacters.Add(new FictionalCharacter("Sonic", "Intermediate", "Hedgehog from a game"));
        intermediateCharacters.Add(new FictionalCharacter("Pikachu", "Intermediate", "Lightning type pokemon"));

    } //Ändra för specifik script

    private void FillAdvancedCharacters()
    {
        advancedCharacters.Add(new FictionalCharacter("Batman", "Advanced", "I am Justice"));
        advancedCharacters.Add(new FictionalCharacter("Kratos", "Advanced", "Angy god"));
        advancedCharacters.Add(new FictionalCharacter("Gandalf", "Advanced", "Dumbledore?"));   
        
    } //Ändra för specifik script

    public List<FictionalCharacter> GetCharacters(string difficulty) //Denna kod behöver ändras för att passa scenen som spelas och taggen som blir vald. 
    {
        switch (difficulty)
        {
            case "FBeginner":
                return beginnerCharacters;
            case "FIntermediate":
                return intermediateCharacters;
            case "FAdvanced":
                return advancedCharacters;
            default:
                return null;
        }
    }

    public void SetLetterBoxes() //Det här Sätter fram den synliga basen för alla bokstäver, ÄNDRA EJ
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

    private string GetRandomName(List<FictionalCharacter> characters)
    {
        if (characters.Count == 0)
        {
            Debug.LogError("List is empty!");
            return "";
        }

        randomIndex = Random.Range(0, characters.Count);
        return characters[randomIndex].name;
    } //Det här drar olika ord varje gång det spelas, ÄNDRA EJ (förutom vilken slags lista)

    void ReadPlayerInput()  //Läser in spelaren input (Vad den skriver), denna kod behöver EJ ÄNDRAS den passar i alla scripts. Dock kan kopplingen med timer.SetDifficulty() ändras för att passa annan script.
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
    
    private void AttemptGuess(int chances) //Begränsar antal försök spelaren har och även sätter fram vinst/förlust utkomst.
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

    void SetLetterWords(int numberOfLetters) //Sätter fram spelarens input för synlighetsskull ÄNDRA EJ
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

    public string GetHint(List<FictionalCharacter> hint) //Då click delarna är en del av categories, referera till den inom denna metod. Inom denna ska det finnas setActive för popupen samt texten
    { //Eller gör en seperat script bara för hint knappen då det inte trasslas med andra delar.
        if (hint == null)
        {
            Debug.LogError("there is no hint!");
            return "";
        }
        
        hintText.text = hint[randomIndex].hint;
        return hintText.text;
    }
}
