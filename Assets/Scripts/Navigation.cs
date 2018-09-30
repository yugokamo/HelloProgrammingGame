using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour {
    [SerializeField] private Text talkText;
    [SerializeField] private GameObject balloon;
    [SerializeField] private GameObject enemys;
    private List<List<string>> talks = new List<List<string>>();
    private Coroutine currentTalkCoroutine;
    private Vector3 startPosition;
    private enum Step {
        None = -1,
        Step1,
        Step2,
        Step3,
        Step4
    }
    private Step step;
    private const float StepJudgeIntervalTime = 1.0f;
    private const float talkIntervalTime = 0.8f;
    private const float talkDisplayTime = 3.2f;
    private const float notMovingPositionX = -4.0f;
    private const float tooFastMovingPositionX = 10.0f;
    private const float step4ClearPositionX = 48.0f;

	// Use this for initialization
    private IEnumerator Start () {
        step = Step.None;
        startPosition = transform.position;
        talks.Add(new List<string>(){
            "あ、そこのきみ！",
            "ちょうどよかった！",
            "きみは見習いエンジニアだね",
            "早速なんだけど、\nぼくをたすけてほしい",
            "なんだよ、\nめんどうそうな顔するなよ",
            "ぼくはこのゲームの\n主人公なのだけど",
            "さっきから一歩も\n前に進めないんだ！",
            "むだな足踏みはもうこりごりだよ",
            "前進するコードを書いて\n僕を動かしてほしい",
            "テキストのChapter1がヒントだよ",
        });
        talks.Add(new List<string>(){
            "おお！やっと前に進めた！\nって、速すぎるよおお！！",
            "テキストのChapter2をヒントに\nスピードを調整して！",
        });
        talks.Add(new List<string>(){
            "おお！やっと前に進めた!",
            "速さもいいかんじ！",
            "ん？",
            "え、ちょっと..",
            "どいてもらってもいいですか",
            "・・・",
            "まっすぐ歩くだけじゃ\nダメみたいだ",
            "下にジャンプボタンがあるけど・・",
            "押しても反応がないみたい・・",
            "チラッ",
            "きみだけがたよりだ！",
            "きみのちからで\nジャンプを実装してほしい",
            "テキストのChapter3を\nヒントにやってみて！"
        });
        talks.Add(new List<string>(){
            "障害物をジャンプでよけれた！",
            "これで何でも避けれるぞ",
            "・・・",
            "って、高さが足りないー！",
            "大ジャンプと小ジャンプ",
            "2つのボタンをつくってほしい",
            "テキストのChapter4\nヒントにやってみて！"
        });
        yield return new WaitForSeconds(StepJudgeIntervalTime);
        if (transform.position.x < notMovingPositionX) {
            step = Step.Step1;
        } else if (Vector3.Distance(transform.position, startPosition) > tooFastMovingPositionX) {
            step = Step.Step2;
            enemys.SetActive(false);
        } else {
            step = Step.Step3;
        }
        currentTalkCoroutine = StartCoroutine(StartTalk(talks[(int)step]));
	}

	// Update is called once per frame
	void Update () {
        if (transform.position.x > step4ClearPositionX && step == Step.Step3) {
            step = Step.Step4;
            StopCoroutine(currentTalkCoroutine);
            StartCoroutine(StartTalk(talks[(int)step]));
        }
	}

    private IEnumerator StartTalk(List<string> targetTalks) {
        while (targetTalks.Count > 0) {
            balloon.SetActive(false);
            yield return new WaitForSeconds(talkIntervalTime);
            balloon.SetActive(true);
            talkText.text = targetTalks[0];
            yield return new WaitForSeconds(talkDisplayTime);
            targetTalks.RemoveAt(0);
        }
    }
}
