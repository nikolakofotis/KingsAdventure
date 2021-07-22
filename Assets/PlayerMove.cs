using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

using TMPro;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D player;
    public BulletScript bullet;
    private float timeNew,jumpTime;
    public bool hasHeli, move, isGrounded, attack, blockMovement, isDead, maxLives, newGame, loadingInLvl, enterDoor, load,touchDown,push;
    public float speed, jumpForce, lives, force, diamonds,hasRocks;
    public string direction;
    private Vector2 jump;
    public Animator animator;
    public Transform hammerStart, hammerEnd, spawnPoint, feet, throwRockPos;
    public CrateScript crate;
    private float timeNow, timeNow1, timeN,timeNow3;
    private PigScript enemy;
    private IEnumerator attackWait;
    public int weapon;
    public float weaponDamage;
    public Pooling pool;
    public UIScript ui;
    public bool interact;
    public int level, bulletCount,currentLevel,nextLevel;
    public float[] doorPos, checkpointPos;
    public TextMeshProUGUI txt, heartTXT , rockDisplayTXT;
    public int[] hasWeapons;
    public int usableHearts;
    public GameObject heartButton;
    public Canvas deadCanvas;
    public List<string> diamond;
    private Transform ptransform;
    public bool changeArray;
    public GameObject[] allDiamonds,allDialogues;
    public  string[] tempArray;
    public new CameraScript camera;
    private bool blockJump;
    public AudioClip footsteps, jumpSound ,rangedSound,healSound,dashclip;
    public AudioClip[] landingSounds,attackSounds,painSounds,meleeSounds,crateSounds;
    public AudioSource footstepsJumpSource, landingSource, attackSource, hitSource, takeDmgSource,remoteSource1,remoteSource2,levelSoundSource;
    public List<string> dialogues;
    public  string[] tempDiag;
    private bool isFalling,doorIn;
    private float temp1Jump, temp2Jump ;
    public int hasKeyForLevels;
    private bool throwRock;
    public int[] hasBottles;
    public int hasMadeBottles;
    public LoadingScreenScript loadingScreen;
    public bool hasEndedTheGame;
    public Canvas endGameCanvas;
    private string  getDir;
    private bool secondJump;
    public Canvas passedGameCanvas;
    public ParticleSystem jumpParticles;
    public string bulletType;
    public string lastSetPortal;
    public Queue<GameObject> portals = new Queue<GameObject>();
    public GameObject portalEnter,portalExit;
    private float currentDiamonds,diamondsAfairesh;
    private int purpleCount;
    public GameObject purpleUI;
    public TextMeshProUGUI purpleUIText;
    public GameObject ground,dashParticles;
    public TextMeshProUGUI dashText;
    private bool dashLock;
    private float dashTimer;
    public string language;
    private bool enterJump;
    private bool dieOnce;
    public int diedTimes;
    public TextMeshProUGUI triesText;
    public  TMPro.TMP_FontAsset greek, english;
    public float tempDiamonds, tempHearts, tempBullets;
    public AudioClip level1, level2, boss, final,win;
    private bool playDead;
    public TextMeshProUGUI loadLastCheckText;
    public PolygonCollider2D myCollider;
    public  UIScript uiElements;












    void Start()
    {
        uiElements = GameObject.Find("UIElements").GetComponent<UIScript>();
        myCollider = gameObject.GetComponent<PolygonCollider2D>();
        playDead = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        jumpParticles.gameObject.SetActive(false);
        diedTimes = 0;
        dieOnce = true;
        enterJump = true;
        dashTimer = 5f;
        dashLock = false;
        purpleCount = 26;
        portalEnter = null;
        portalExit = null;
        secondJump = false;
        hasEndedTheGame = false;
        hasMadeBottles = 0;
        levelSoundSource.volume = 0.7f;
        hasBottles = new int[4];
        hasWeapons = new int[8];
        checkpointPos = new float[3];
        speed = 3.75f;
        force = 0.5f;
        lives = 6f;
        move = false;
        jumpForce = 26.55f;
        jump = new Vector2(0f, jumpForce);
        isGrounded = true;
        blockMovement = false;
        attack = false;
        isDead = false;
        interact = false;
        enterDoor = false;
        load = false;
        touchDown = true;
        hasRocks = 50f;
        hasKeyForLevels = 1;
        getDir = "right";
        levelSoundSource.volume = 0.3f;
        jumpParticles.Pause();
        bulletType = "normal";
        lastSetPortal = "out";
        
      
        



        deadCanvas.gameObject.SetActive(false);

        LoadNewLvl();
        WeaponCheck();
        tempDiamonds = diamonds;
        tempHearts = usableHearts;
        tempBullets = bulletCount;
        if(bulletCount<25)
        {
            bulletCount = 25;
        }
        uiElements.CheckLives(lives);
        uiElements.SetDiamondText(diamonds);
        camera = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        txt = GameObject.Find("BulletsText").GetComponent<TextMeshProUGUI>();
        txt.text = bulletCount.ToString() + " X";
        ground = GameObject.Find("Ground");
        animator.SetInteger("Weapon", weapon);
        // currentDiamonds = diamonds;







    }

    
    void FixedUpdate()
    {
        

        purpleUIText.text = purpleCount.ToString() + " x";
        timeNow = Time.deltaTime + timeNow;
        timeNow1 = Time.deltaTime + timeNow1;
        timeNow3 = Time.deltaTime + timeNow3;
        timeN = Time.deltaTime + timeN;
        jumpTime = Time.deltaTime + jumpTime;
        
        if (usableHearts > 0)
        {
            heartButton.SetActive(true);
            heartTXT.text = usableHearts.ToString();
        }
        else if (usableHearts <= 0)
        {
            heartButton.SetActive(false);
        }


        timeNew = Time.deltaTime + timeNew;
        //animator.SetBool("DoorIn", doorIn);
        // shop = GameObject.Find("Shop");

       
        rockDisplayTXT.text = hasRocks.ToString() + " X";
        //txt.text = bulletCount.ToString() + " X";
        
        animator.SetBool("Attack", attack);
        animator.SetBool("Movement", move);
        animator.SetBool("Jump", !isGrounded);
        animator.SetBool("IsDead", isDead);
        animator.SetBool("EnterDoor", enterDoor);

        animator.SetBool("isFalling", isFalling);
        animator.SetBool("ThrowRock", throwRock);



        if (blockMovement == false)
        {


            JumpCheck();
            Move(direction);
            FeetCheck();
        }

    }

    public bool CheckMusic()
    {
        if (levelSoundSource.isPlaying)
        {
            return true;
        }
        else return false;
    }

    public void Move(string s)
    {
        
        

            if (s == "right" && speed < 0)
            {
                getDir = "right";
                speed = -speed;
                force = -force;
                transform.eulerAngles = new Vector2(0, 0);
            }
            else if (s == "left" && speed > 0)
            {
                getDir = "left";
                speed = -speed;
                force = -force;
                transform.eulerAngles = new Vector2(0, 180f);
            }



            if (move)
            {
                transform.position = new Vector2(player.position.x + speed * Time.fixedDeltaTime, player.position.y);
                float delay = 0.45f;
                if (isGrounded)
                {
                    if (move)
                    {
                        PlayFootsteps();
                        if (timeNow3 > delay)
                        {
                            timeNow3 = 0f;

                            GameObject foot2 = pool.Spawn("FootSteps", player.position + new Vector2(0.5f * (force / Mathf.Abs(force)) * Time.fixedDeltaTime, -0.5f), Quaternion.identity);
                            pool.EnQ("FootSteps", foot2, 0.4f);
                            StartCoroutine(MoveHelp());

                        }
                    }

                }

            }

        }
    


    public string  GetDirection()
    {
        return getDir ;
    }

    public IEnumerator MoveHelp()
    {
        yield return new WaitForSeconds(0.225f);
        GameObject foot1 = pool.Spawn("FootSteps", player.position + new Vector2(0.5f * (force / Mathf.Abs(force)) * Time.fixedDeltaTime, -0.5f), Quaternion.identity);
        pool.EnQ("FootSteps", foot1, 0.4f);
        


    }
    public void MoveDirection(bool mov)
    {

        move = mov;
       

    }
    
    
    public void Jump()
    {
        
        if (enterJump)
        {
            enterJump = false;
            if (isGrounded && !blockJump)
            {

                secondJump = true;
                player.AddForce(jump, ForceMode2D.Impulse);
                SoundSystem.PlaySound(jumpSound, false, 0.4f,footstepsJumpSource);
                isGrounded = false;
                blockJump = true;
                StartCoroutine(UnblockJump());
                //StartCoroutine(WaitU());
            }
            else if (!isGrounded && secondJump)
            {
                secondJump = false;
                jumpParticles.gameObject.SetActive(true);
                if (player.velocity.y < 0)
                {
                    player.AddForce(jump - new Vector2(0f, player.velocity.y), ForceMode2D.Impulse);

                }
                else
                {
                    player.AddForce(jump, ForceMode2D.Impulse);
                }
                jumpParticles.Play();
                SoundSystem.PlaySound(jumpSound, false, footstepsJumpSource);
                isGrounded = false;
                blockJump = true;
                StartCoroutine(UnblockJump());
            }
        }
    }

    public void UnblockJumpButton()
    {
        enterJump = true;
    }


  private  IEnumerator UnblockJump()
    {
        yield return new WaitForSeconds(0.5f);
        blockJump = false;

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Crate" || collision.gameObject.CompareTag("Cannon") || collision.gameObject.CompareTag("Steps") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("ShadowPig") || collision.gameObject.CompareTag("CannonHead") || collision.gameObject.CompareTag("PushRock") || collision.gameObject.CompareTag("DashCrate"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                touchDown = true;
                isFalling = false;
            }
        }
        else if(collision.gameObject.CompareTag("SideWalls"))
        {
            move = false;
           
            isGrounded = false;

        }


        if(push)
        {
            if(collision.gameObject.CompareTag("Crate"))
            {
                CrateScript crate = collision.gameObject.GetComponent<CrateScript>();
                SoundSystem.PlaySound(dashclip, false, hitSource);
                crate.DashHit();
                crate.hitCrate(force * 2);
            }
            else if(collision.gameObject.CompareTag("Blackbird"))
            {
                SoundSystem.PlaySound(dashclip, false, hitSource);
                collision.gameObject.GetComponent<BlackbirdScript>().Die();
            }
            else if(collision.gameObject.CompareTag("ShadowPig"))
            {
                SoundSystem.PlaySound(dashclip, false, hitSource);
                collision.gameObject.GetComponent<ShadowPig>().takeDamage(force*3f, 50f);
            }
            else if(collision.gameObject.CompareTag("DashCrate"))
            {
                SoundSystem.PlaySound(dashclip, false, hitSource);
                collision.gameObject.GetComponent<DashCrateScript>().TakeDamage();
            }
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(!isGrounded)
            {
                isGrounded = true;
                isFalling = false;
               
            }
        }
    }



    public void Attack()
    {
        if (!push)
        {
            if (bulletCount > 0 && bulletType == "normal")
            {

                bulletCount = bulletCount - 1;
                StartCoroutine(camera.Shake(0.25f));
                SoundSystem.PlaySound(rangedSound, false, hitSource);
                GameObject bulletPrefab = pool.Spawn("Bullet", spawnPoint.position, Quaternion.identity);
                bulletPrefab.GetComponent<BulletScript>().OnSpawn(force);
                GameObject bulletParticle = pool.Spawn("BulletParticle", spawnPoint.position, Quaternion.identity);
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force * 2f, 0f), ForceMode2D.Impulse);
                pool.EnQ("BulletParticle", bulletParticle, 1.5f);
            }
            else if (bulletType == "blue")
            {

                bulletCount = bulletCount - 1;
                StartCoroutine(camera.Shake(0.25f));
                SoundSystem.PlaySound(rangedSound, false, hitSource);
                GameObject bulletPrefab = pool.Spawn("BlueBullet", spawnPoint.position, Quaternion.identity);
                bulletPrefab.GetComponent<BulletScript>().OnSpawn(force);
                GameObject bulletParticle = pool.Spawn("BlueBulletParticle", spawnPoint.position, Quaternion.identity);
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force * 2f, 0f), ForceMode2D.Impulse);
                pool.EnQ("BlueBulletParticle", bulletParticle, 1.5f);
            }
            else if (bulletType == "purple")
            {


                if (purpleCount > 0)
                {
                    bulletCount = bulletCount - 1;
                    StartCoroutine(camera.Shake(0.25f));
                    SoundSystem.PlaySound(rangedSound, false, hitSource);
                    GameObject bulletPrefab = pool.Spawn("PurpleBullet", spawnPoint.position, Quaternion.identity);
                    bulletPrefab.GetComponent<BulletScript>().OnSpawn(force);
                    PurpleBulletScript portalBehavior = bulletPrefab.GetComponent<PurpleBulletScript>();
                    if (lastSetPortal == "out")
                    {
                        portalBehavior.SetPortalDoor("in");
                        lastSetPortal = "in";
                    }
                    else
                    {
                        portalBehavior.SetPortalDoor("out");
                        lastSetPortal = "out";
                    }
                    GameObject bulletParticle = pool.Spawn("PurpleShootParticle", spawnPoint.position, Quaternion.identity);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force * 2f, 0f), ForceMode2D.Impulse);
                    pool.EnQ("PurpleShootParticle", bulletParticle, 1.5f);

                }
                if (purpleCount != 0)
                {
                    purpleCount--;
                }
                
            }

            


        }
        txt.text = bulletCount.ToString() + " X";
    }

    public void SetPush(bool set)
    {
        push = set;
    }

   


    public void LoseLife(float lifeToLose)
    {
        lives = lives - lifeToLose;
        uiElements.CheckLives(lives);
        blockJump = true;
        int random = Random.Range(0, 5);
        SoundSystem.PlaySound(painSounds[random], false, takeDmgSource);
        StartCoroutine(camera.Shake(0.25f));
        if (lives <= 0f && dieOnce)
        {
            
                diedTimes++;
          if(diedTimes==3)
            {
                loadLastCheckText.text = "LOAD LAST CHECKPOINT (AD)";
            }
          else
            {
                loadLastCheckText.text = "LOAD LAST CHECKPOINT";
            }
                playDead = true;
                dieOnce = false;
                SaveSystem.Save(this);
                isDead = true;
            animator.SetBool("IsDead", isDead);
            blockMovement = true;
                StartStopMusic("stop");


                gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(0f, 0.3f);
            


        }
        else
        {
            animator.SetBool("loseLife", true);
        }
        StartCoroutine(DeadSetActive());
    }

    public void ChangeMusic(AudioClip clip)
    {
        SoundSystem.PlaySound(clip, true, levelSoundSource);
    }
    private IEnumerator DeadSetActive()
    {


        if (isDead && playDead)
        {
            
            playDead = false;
            blockJump = false;
            yield return new WaitForSeconds(1f);
            deadCanvas.gameObject.SetActive(true);
            DisplayTries();
            Time.timeScale = 0f;
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("loseLife", false);
            blockJump = false;
        }

    }

    private void DisplayTries()
    {
        if(language=="el")
        {
            
            triesText.font = greek;
            triesText.text = "Προσπάθειες που απομένουν: " + (3 - diedTimes).ToString();
        }
        else
        {
            triesText.font = english;
            triesText.text = "Tries remaining: " + (3 - diedTimes).ToString();
        }
    }
    public void Ressurect()
    {
        uiElements.CheckLives(lives);
        if (diedTimes <= 2)
        {

            playDead = false;
            isDead = false;
            animator.SetBool("IsDead", false);
            dieOnce = true;
            blockMovement = false;
            StartStopMusic("start");
            lives = 6f;
            txt.text = bulletCount.ToString() + " X";
            // deadCanvas.gameObject.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(-0.25f, 0f);
        }
        else if(diedTimes==3)
        {
            gameObject.GetComponent<AdScript>().ShowRewardedAd();
            //RestartLevel();
        }
    }

    public void AdRessurect()
    {
        blockMovement = false;
        playDead = false;
        isDead = false;
        dieOnce = true;
        lives = 6f;
        //StartCoroutine(PlayMusicWithDelay());
    }

    private IEnumerator PlayMusicWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        StartStopMusic("start");
    }

    public void WeaponCheck()
    {


       /* diamondsAfairesh = diamonds - currentDiamonds;
        if(diamondsAfairesh==2)
        {
            diamondsAfairesh = 0;
            bulletCount += 1;
            currentDiamonds = diamonds;
        }*/
           
         gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(-0.25f, 0f);
         weaponDamage = 40f;
           
        
    }

    public void GetDiamond()
    {
        diamonds++;
        currentDiamonds++;
        if(currentDiamonds==2)
        {
            currentDiamonds = 0;
            bulletCount++;
            txt.text = bulletCount.ToString() + " X";
        }
        uiElements.SetDiamondText(diamonds);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            interact = true;
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            DoorScript door = collision.gameObject.GetComponent<DoorScript>();
            if (door.enter)
            {
                if (hasKeyForLevels >= door.isDoorForLevel)
                {
                    door.doorButton.SetActive(true);
                }
                else door.doorButton.SetActive(false);
            }

        }
        else if (collision.gameObject.CompareTag("CheckPoint"))
        {

            checkpointPos[0] = collision.gameObject.transform.position.x;
            checkpointPos[1] = collision.gameObject.transform.position.y;
            AutoSave();

        }
       


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            interact = false;
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            DoorScript door = collision.gameObject.GetComponent<DoorScript>();
            if (door.enter)
            {
                door.doorButton.SetActive(false);
            }

        }else if( collision.gameObject.CompareTag("Steps"))
        {
            isGrounded = false;
            StartCoroutine(WaitU());
        }
        else if (collision.gameObject.CompareTag("ColliderHelp"))
        {

            
            isGrounded = true;
            isFalling = false;

        }
    }
    
    public void EnterLevel(int lvl)
    {

        enterDoor = true;
        animator.SetBool("EnterDoor", enterDoor);
        currentLevel = level;
        nextLevel = lvl;
        changeArray = true;
        PlayLoadingScreen();
        SaveSystem.Save(this);
        SaveSystem.SaveWorld(this, level);



        StartCoroutine(EnterLevelDelay(lvl));



    }

    public void AdEnterLevel(int lvl)
    {
        enterDoor = true;
        currentLevel = level;
        nextLevel = lvl;
        changeArray = true;
        SaveSystem.Save(this);
        SaveSystem.SaveWorld(this, level);



        SceneManager.LoadScene(lvl.ToString());

    }

    public IEnumerator EnterLevelDelay(int lvl)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(lvl.ToString());




    }

    public void ExitLevel()
    {
        level = 0;
        PlayLoadingScreen();
        SaveSystem.Save(this);
        SceneManager.LoadScene("0");

    }

    private void AutoSave()
    {
        newGame = false;
        changeArray = true;

        SaveSystem.SaveWorld(this, level);
        SaveSystem.Save(this);





    }

    public void LoadLastSave()
    {
       
       
        Time.timeScale = 1f; 
        PlayerData data = SaveSystem.Load();
        hasBottles = data.hasBottles;
        hasMadeBottles = data.hasMadeBottles;
        float x = data.checkpoints[0];
        float y = data.checkpoints[1];
        transform.position = new Vector3(x, y, 0f);
        //level = data.nextLevel;

        lives = 6f;
        weapon = data.weapon;
        hasWeapons = data.hasWeapons;
        bulletCount = data.bulletCount;
        usableHearts = data.usableHearts;
        txt.text = bulletCount.ToString() + " X";




        load = true;
        GameObject[] tempG = GameObject.FindGameObjectsWithTag("Steps");

        foreach(GameObject g in tempG)
        {
            g.GetComponent<StepsScript>().ResetSteps();
        }

        StartCoroutine(LoadWait());
        Ressurect();


    }

    private IEnumerator LoadWait()
    {
        yield return new WaitForSeconds(0.25f);
        deadCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        load = false;
    }

    public void RestartLevel()
    {
        float ran = Random.value;
        
        if (ran <= 0.26f)
        {
            gameObject.GetComponent<AdScript>().ShowNormalAd();
        }
        else
        {
            Time.timeScale = 1f;
            diamonds = tempDiamonds;
            bulletCount = (int)tempBullets;
            usableHearts = (int)tempHearts;
            diedTimes = 0;
            Ressurect();
            lives = 6f;
            SaveSystem.Save(this);
            EnterLevel(level);
        }
    }

    public void ReturnToSafeRoom()
    {
        Time.timeScale = 1f;
        lives = 6f;
        diedTimes = 0;
        SaveSystem.Save(this);
        PlayLoadingScreen();
        Ressurect();
        SceneManager.LoadScene("0");
    }
   

   
    public void LoadNewLvl()
    {
        PlayerData data = SaveSystem.Load();
        level = data.nextLevel;
        hasKeyForLevels = data.levelsCheck;
        WorldData wrlData = SaveSystem.LoadWorld(level);
        allDiamonds = GameObject.FindGameObjectsWithTag("Diamonds");
        language = data.language;
        newGame = data.newGame;
        hasBottles = data.hasBottles;
        hasMadeBottles = data.hasMadeBottles;
        changeArray = true;

      

        if (!newGame)
        {
            if ( wrlData!=null)
            {
                
                    tempArray = new string[wrlData.diamond.Length];
                    tempArray = wrlData.diamond;
                    if (tempArray.Length > 0)
                    {
                        for (int x = 0; x < tempArray.Length; x++)
                        {
                        
                        diamond.Add(tempArray[x]);
                             for(int y=0;y<allDiamonds.Length;y++)
                             {
                                    if(tempArray[x]==allDiamonds[y].name)
                                    {
                                //allDiamonds[y].SetActive(false);        



                                    }


                             }
                        }
                    }
                
            }

         
        }

        CheckDiag();

        doorPos = new float[2];
        //SoundSystem.PlaySound(levelSounds[level], true, levelSoundSource);
        switch (level)
        {
           
            case 0:
                SoundSystem.PlaySound(final, true, levelSoundSource);
                doorPos[0] = -30f;
                doorPos[1] = 1.3f;

                break;
            case 1:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -30.5f;
                doorPos[1] = 1.5f;
                break;
            case 2:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -37.2f;
                doorPos[1] = -39.6f;
                break;

            case 3:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -32.89f;
                doorPos[1] = -40.933f;
                break;
            case 4:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -34.26597f;
                doorPos[1] = -39.65588f;
                break;
            case 5:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -45.15223f;
                doorPos[1] = -16.24578f;
                break;
            case 6:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -32.00996f;
                doorPos[1] = -39.60389f;
                break;
            case 7:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -30.09f;
                doorPos[1] = 1.54f;
                break;
            case 8:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -30.09f;
                doorPos[1] = 1.54f;
                break;
            case 9:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -30.09f;
                doorPos[1] = 1.54f;
                break;
            case 10:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -30.02434f;
                doorPos[1] = 1.225048f;
                break;
            case 11:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -29.80793f;
                doorPos[1] = 0.462722f;
                break;
            case 12:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -29.67461f;
                doorPos[1] = 0.2306852f;
                break;
            case 13:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -29.67461f;
                doorPos[1] = 0.2306852f;
                break;
            case 14:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -29.82321f;
                doorPos[1] = -0.460614f;
                break;
            case 15:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -28.78489f;
                doorPos[1] = -0.229847f;
                break;
            case 16:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -27.72602f;
                doorPos[1] = -2.662108f;
                break;
            case 17:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -27.39394f;
                doorPos[1] = -3.016081f;
                break;
            case 18:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -27.28f;
                doorPos[1] = -2.4096f;
                break;
            case 19:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -26.87659f;
                doorPos[1] = -1.91063f;
                break;
            case 20:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -28.07163f;
                doorPos[1] = -2.199462f;
                break;
            case 21:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -27.02304f;
                doorPos[1] = -1.974822f;
                break;
            case 22:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -28.11779f;
                doorPos[1] = -2.599926f;
                break;
            case 23:
                SoundSystem.PlaySound(level1, true, levelSoundSource);
                doorPos[0] = -27.67085f;
                doorPos[1] = -1.788533f;
                break;
            case 24:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -29.45623f;
                doorPos[1] = -2.041645f;
                break;

            case 25:
                SoundSystem.PlaySound(level2, true, levelSoundSource);
                doorPos[0] = -27.37719f;
                doorPos[1] = -1.981769f;
                break;
           


        }
        transform.position = new Vector2(doorPos[0], doorPos[1]);



        if (newGame)
        {
            
            for (int i = 0; i < hasWeapons.Length; i++)
            {
                hasWeapons[i] = 0;
            }
            diamonds = data.diamonds;
            weapon = data.weapon;
            bulletCount = data.bulletCount;
            usableHearts = data.usableHearts;
        }
        else if (newGame == false)
        {

            if (data.levelsCheck==666)
            {
                GameObject.Find("Controls").SetActive(false);
                hasEndedTheGame = false;
                SaveSystem.Save(this);
                SaveSystem.SaveWorld(this, level);
                SceneManager.LoadScene("EndingDemoScene");




            }
           
          
            lives = data.lives;
            diamonds = data.diamonds;
            weapon = data.weapon;
            hasWeapons = data.hasWeapons;
            bulletCount = data.bulletCount;
            usableHearts = data.usableHearts;
        }
       
        
       
        SaveSystem.Save(this);
        SaveSystem.SaveWorld(this, level);



    }

    private void CheckDiag()
    {
        WorldData wrlData = SaveSystem.LoadWorld(level);
        allDialogues = GameObject.FindGameObjectsWithTag("Dialogue");
        if (wrlData !=null)
        {

            tempDiag = new string[wrlData.dialogues.Length];
            tempDiag = wrlData.dialogues;
            if (tempDiag.Length > 0)
            {
                for (int x = 0; x < tempDiag.Length; x++)
                {

                    dialogues.Add(tempDiag[x]);
                    for (int y = 0; y < allDialogues.Length; y++)
                    {
                        if (tempDiag[x] == allDialogues[y].name)
                        {
                            allDialogues[y].SetActive(false);



                        }


                    }
                }
            }
        }
    }


    public bool HasBoughtWeapons(int weaponNumber)
    {
        foreach (float x in hasWeapons)
        {
            if (x.Equals(weaponNumber))
            {
                return true;
            }
        }

        return false;


    }

    public void HeartIncrease()
    {

        if (lives == 6f) maxLives = true;

        else maxLives = false;

        if (maxLives == false)
        {
            SoundSystem.PlaySound(healSound, false, remoteSource1);
            lives = lives + 1;
            usableHearts = usableHearts - 1;
            uiElements.CheckLives(lives);
        }
    }

    public void GotObject(GameObject obj)
    {
        string x = obj.name;
        diamond.Add(x);
        diamond.Sort();

    }

    public void GotDialog(GameObject diag)
    {
        string x = diag.name;
        dialogues.Add(x);
        dialogues.Sort();

    }

   

   public void RemoteSave()
    {
        SaveSystem.Save(this);
    }

   private IEnumerator WaitU()
    {
        yield return new WaitUntil(() =>isGrounded);
        float delay = 1f;
        if (timeN > delay)
        {
            int x = Random.Range(0, 3);
            timeN = 0f;
            SoundSystem.PlaySound(landingSounds[x], false, landingSource);
            GameObject jPart = pool.Spawn("JumpParticles", gameObject.transform.position + new Vector3(0f, -0.6f, 0f), Quaternion.identity);
            pool.EnQ("JumpParticles", jPart, 0.5f);
        }

       

    }

   
    private  void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cannon"))
        {
            touchDown = false;
            isGrounded = false;
            StartCoroutine(WaitU());

        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            StartCoroutine(WaitU());

        }
       

    }

   

    private IEnumerator ExitingGround()
    {
        yield return new WaitForSeconds(0.2f);
        isGrounded = false;
    }
   

    public void PlayFootsteps()
    {

        if (timeNew > 0.3f && isGrounded && touchDown)
        {
            timeNew = 0;
            footstepsJumpSource.clip = footsteps;
            footstepsJumpSource.clip = footsteps;
            footstepsJumpSource.Play();

        }


    }


    public void  PlayRemoteSound(AudioClip clip,int soundsource)
    {
        if (soundsource == 1)
        {

            SoundSystem.PlaySound(clip, false, remoteSource1);
        }
        else if (soundsource == 2)
        {

            SoundSystem.PlaySound(clip, false, remoteSource2);
        }
    }

    public IEnumerator RemoteReappear(GameObject objToReap,float live)
    {
        int x = Random.Range(2, 5);
        objToReap = pool.Spawn("ShadowPig", new Vector3(transform.position.x + x *PlayerRelativeTo(objToReap), transform.position.y, transform.position.z), Quaternion.identity);
        ShadowPig script = objToReap.GetComponent<ShadowPig>();
        GameObject temp=  Instantiate(script.shadowCloud, objToReap.transform.position, Quaternion.identity);
        int y = Random.Range(2, 4);
        Debug.Log(y);
        yield return new WaitForSeconds(2);
        
        script.dissapearLock = false;
        Destroy(temp, 0.5f);
        script.SetLives(live);

    }


    private int PlayerRelativeTo(GameObject g)
    {
        if (transform.position.x > g.gameObject.transform.position.x)
        {
            return 1;
        }
        else if (transform.position.x < g.gameObject.transform.position.x)
        {
            return -1;
        }
        else return 0;
    }


   


    private void JumpCheck()
    {
        float delay = 0.025f;
       
        if (!isGrounded)
        {
           
            if (jumpTime > delay)
            {
                jumpTime = 0f;
                temp2Jump = transform.position.y;
                if (temp1Jump > temp2Jump)
                {
                    isFalling = true;
                   
                }

            }
            
            temp1Jump = temp2Jump;
        }
        
        
    }

    

    public void StartStopMusic(string startOrStop)
    {
        if(startOrStop=="stop")
        {
            levelSoundSource.Pause();
        }
        else if(startOrStop=="start")
        {
            levelSoundSource.Play();
        }
    }

    public int PlayerIsLooking()
    {
        if(transform.rotation.y==0)
        {
            
            return -1;
        }
        else if(transform.rotation.y == -1)
        {
           
            return 1;
           
            
        }

        return 0;
    }

   
    public void EnteringDoorTeleport(DoorScript exit)
    {

        enterDoor = true;
        
        StartCoroutine(DoorWait(exit));

    }

    private IEnumerator DoorWait(DoorScript exit)
    {
        yield return new WaitForSeconds(0.5f);
        enterDoor = false;
        
        transform.position = new Vector3(exit.transform.position.x, exit.transform.position.y - 1f, exit.transform.position.z); 
        
    }

    public void SetKeyLevel(int levelUnlock)
    {
        if (hasKeyForLevels < levelUnlock && levelUnlock!=666)
        {
            hasKeyForLevels = levelUnlock;
            SaveSystem.Save(this);
        }
        else if(levelUnlock==666)
        {
            
            hasKeyForLevels = levelUnlock;
            hasEndedTheGame = true;
        }
    }


    public int GetKeyLevel()
    {
        return hasKeyForLevels;
    }




    public void ThrowRocks()
    {
        if (hasRocks > 0 && isGrounded && !throwRock)
        {
            int rand = Random.Range(0, 2);
            
            hasRocks--;
            GameObject obj = pool.Spawn("Rocks", throwRockPos.position, Quaternion.identity);
            Rigidbody2D objBody = obj.GetComponent<Rigidbody2D>();
            float r = Random.Range(5f, 7f);
            objBody.AddForce(new Vector2((force / Mathf.Abs(force)) * r, 5f), ForceMode2D.Impulse);
            throwRock = true;
            StartCoroutine(ThrowRocksHelp());
        }
        

    }

    private IEnumerator ThrowRocksHelp()
    {
        yield return new WaitForSeconds(0.25f);
        throwRock = false;
    }

    public void GetBottles(int bottleCode)
    {
       
            hasBottles[bottleCode]++;
        
       
    }


    public void ResetBottles()
    {
        hasBottles = new int[4];
        for(int x=0;x<hasBottles.Length;x++)
        {
            hasBottles[x] = 0;
        }
    }

    private void PlayLoadingScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        
       
    }



    public void PassedLevel(string select)
    {
        if (select == "shop")
        {
            EnterLevel(0);
        }
        else if(select=="next")
        {
            float rand = Random.value;
            if(rand<=0.4f)
            {
                gameObject.GetComponent<AdScript>().PassedLevelAd();
            }
            else
            {
                EnterLevel(level + 1);
            }
            
        }
    }


    public void OpenPassedLevel()
    {
        passedGameCanvas.gameObject.SetActive(true);
        SoundSystem.PlaySound(win, true, levelSoundSource);
    }


    public void teleportPortals( )
    {
        if (portalExit != null && portalExit.GetComponent<PortalScript>().portalType!="neutral")
        {
            transform.position = portalExit.transform.position;
            load = true;
            StartCoroutine(TeleportLoadingDisable());
        }
    }

    private IEnumerator TeleportLoadingDisable()
    {
        yield return new WaitForSeconds(1f);
        load = false;
    }


   public void SetNewPortals(  GameObject gameobject)
    {
        portals.Enqueue(gameobject);
        GameObject temp;
        if (gameobject.GetComponent<PortalScript>().portalType=="in")
        {
            portalEnter = gameobject;
        }
        else
        {
            portalExit = gameobject;
        }

        if(portals.ToArray().Length>=3)
        {
            temp = portals.Dequeue();
            temp.GetComponent<PortalScript>().portalType = "neutral";
            pool.EnQ("Portal", temp, 0f);
            temp = portals.Dequeue();
            temp.GetComponent<PortalScript>().portalType = "neutral";
            pool.EnQ("Portal", temp, 0f);
        }
       
        
    }

    private void FeetCheck()
    {
        int mask = 1 << 11;
        float rayDirection;
        float rayDirection2;
        if(getDir=="right")
        {
            rayDirection = -0.55f;
            rayDirection2 = 0.13f;

        }
        else 
        {
            rayDirection = 0.55f;
            rayDirection2 =-0.13f;
        }
        RaycastHit2D ray = Physics2D.Linecast(new Vector2(transform.position.x+rayDirection, transform.position.y), new Vector2(transform.position.x+ rayDirection, transform.position.y - 0.75f), mask) ;
        RaycastHit2D ray1 = Physics2D.Linecast(new Vector2(transform.position.x + rayDirection2, transform.position.y), new Vector2(transform.position.x + rayDirection2, transform.position.y - 1.1f), mask);
        
       

        if (ray.collider != null || ray1.collider!=null)
        {
            ground.gameObject.tag = "Ground";
        }
        else
        {
            ground.gameObject.tag = "SideWalls";
        }

       
    }

    public void Dash()
    {
        if (!dashLock)
        {
            //SoundSystem.PlaySound(dashclip, false, hitSource);
            push = true;
            animator.SetBool("Push", push);
            dashLock = true;
            dashParticles.SetActive(true);
            blockMovement = true;
            player.velocity = new Vector2(27f * force, player.velocity.y);
            StartCoroutine(DashUnlock());
        }

    }

    private IEnumerator DashUnlock()
    {
        yield return new WaitForSeconds(0.5f);
        push = false;
        animator.SetBool("Push", push);
        StartCoroutine(StartDashTimer());
        player.velocity = new Vector2(0f, player.velocity.y);
        blockMovement = false;
        dashParticles.SetActive(false);
       
    }

    private IEnumerator StartDashTimer()
    {
        for(int i=1; i>0;i--)
        {


            string temptxt = i.ToString();
            dashText.text = temptxt;
            yield return new WaitForSeconds(1f);
        }
        dashText.text = "";
        dashLock = false;

    }
}


    
    




        
    

   


   
  

    





