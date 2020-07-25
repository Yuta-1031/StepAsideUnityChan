using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	private Animator myAnimator;
	private Rigidbody myRigitbody;
	private float forwardForce = 800.0f;
	private float turnForce = 500.0f;
	private float upForce = 500.0f;
	private float moveleRange = 3.4f;
	private float coefficient = 0.95f;
	private bool isEnd = false;
	private GameObject stateText;
	private GameObject scoreText;
	private int score = 0;
	private bool isLButtonDown = false;
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {
		this.myAnimator = GetComponent<Animator>();
		this.myAnimator.SetFloat("Speed", 1);
		this.myRigitbody = GetComponent<Rigidbody>();
		this.stateText = GameObject.Find("GameResultText");
		this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {

        if (this.isEnd)
        {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
        }

		this.myRigitbody.AddForce(this.transform.forward * this.forwardForce);

		if((Input.GetKey(KeyCode.LeftArrow)||this.isLButtonDown)&&-this.moveleRange < this.transform.position.x)
        {
			this.myRigitbody.AddForce(-this.turnForce, 0, 0);
        }else if((Input.GetKey(KeyCode.RightArrow)||this.isRButtonDown)&& this.transform.position.x < this.moveleRange)
        {
			this.myRigitbody.AddForce(this.turnForce, 0, 0);
        }


        if (Input.GetKey(KeyCode.LeftArrow) && -this.moveleRange < this.transform.position.x)
        {
			this.myRigitbody.AddForce(-this.turnForce, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.moveleRange)
        {
			this.myRigitbody.AddForce(this.turnForce, 0, 0);
        }

        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
			this.myAnimator.SetBool("Jump", false);
        }

		if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < 0.5f)
        {
			this.myAnimator.SetBool("Jump", true);
			this.myRigitbody.AddForce(this.transform.up * this.upForce);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
			this.isEnd = true;
			this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        if (other.gameObject.tag == "GoalTag")
        {
			this.isEnd = true;
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        if (other.gameObject.tag == "CoinTag")
        {
			this.score += 10;
			this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";
			GetComponent<ParticleSystem>().Play();
			Destroy(other.gameObject);
        }
    }


	public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
			this.myAnimator.SetBool("Jump", true);
			this.myRigitbody.AddForce(this.transform.up * this.upForce);
        }
    }

	public void GetMyLeftButtonDown()
    {
		this.isLButtonDown = true;
    }
	public void GetMyLeftButtonUp()
    {
		this.isLButtonDown = false;
    }
	public void GetMyRightButtonDown()
    {
		this.isRButtonDown = true;
    }
	public void GetMyRightButtonUp()
    {
		this.isRButtonDown = false;
    }
}
