using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OffLineRanking : MonoBehaviour
{
    public TextMeshProUGUI rankingDisplayText;
    public TextMeshProUGUI yourRankText; // 自分の順位専用

    public void UpdateRanking()
    {
        int score = MachigaiManager.totalCount;
        RankingUpdate("testRanking", score);
        RankingLoad("testRanking");
    }

    void RankingUpdate(string rankingKey, int newScore)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "名無し");
        string rankingText = PlayerPrefs.GetString(rankingKey, "");
        string[] rankingTextArr = string.IsNullOrEmpty(rankingText) ? new string[0] : rankingText.Split(',');

        List<(string name, int score)> rankingList = new List<(string, int)>();

        bool updated = false;
        foreach (var entry in rankingTextArr)
        {
            string[] parts = entry.Split(':');
            if (parts.Length < 2) continue;
            string name = parts[0];
            int score = int.Parse(parts[1]);

            if (name == playerName)
            {
                score = Mathf.Max(score, newScore); // 自分のスコアは高い方を残す
                updated = true;
            }
            rankingList.Add((name, score));
        }

        if (!updated)
            rankingList.Add((playerName, newScore));

        // スコア順にソート
        rankingList.Sort((a, b) => b.score.CompareTo(a.score));

        // ここでは「全員保存」する（消さない）
        string saveText = string.Join(",", rankingList.ConvertAll(e => e.name + ":" + e.score));
        PlayerPrefs.SetString(rankingKey, saveText);
        PlayerPrefs.Save();
    }

    void RankingLoad(string rankingKey)
    {
        string rankingText = PlayerPrefs.GetString(rankingKey, "");
        string[] rankingTextArr = rankingText.Split(',');
        string displayText = "";

        string playerName = PlayerPrefs.GetString("PlayerName", "名無し");
        int yourRank = -1;

        // 全ランキングをList化
        List<(string name, int score)> rankingList = new List<(string, int)>();
        for (int i = 0; i < rankingTextArr.Length; i++)
        {
            string[] parts = rankingTextArr[i].Split(':');
            if (parts.Length < 2) continue;
            string name = parts[0];
            int score = int.Parse(parts[1]);
            rankingList.Add((name, score));
        }

        // スコア順に並び替え
        rankingList.Sort((a, b) => b.score.CompareTo(a.score));

        // 自分の順位を探す
        for (int i = 0; i < rankingList.Count; i++)
        {
            if (rankingList[i].name == playerName && yourRank == -1)
            {
                yourRank = i + 1; // 順位は1スタート
                break;
            }
        }

        // 上位10件だけ表示
        for (int i = 0; i < Mathf.Min(10, rankingList.Count); i++)
        {
            displayText += $"{i + 1}位：{rankingList[i].name} - {rankingList[i].score}点\n";
        }

        if (rankingDisplayText != null)
            rankingDisplayText.text = displayText;

        if (yourRankText != null)
        {
            if (yourRank > 0)
                yourRankText.text = $"{yourRank}位です!";
            else
                yourRankText.text = "あなたはランク外です";
        }
    }
}
