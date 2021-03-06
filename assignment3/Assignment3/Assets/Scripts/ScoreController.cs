﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public static ScoreController scoreController;
    private int score = 0;
    private UI_Manager uiManager;

	// Use this for initialization
	void Awake () {
        if (scoreController == null) {
            DontDestroyOnLoad(gameObject);
            scoreController = this;
        }
	}

    public int getScore() {
        return score;
    }

    public void addScore(string message, int value) {
        score += value;

        if (uiManager != null) {
            uiManager.addScore(message, value);
        }
    }

    public void setUI_ManagerAndReset(UI_Manager newManager) {
        this.uiManager = newManager;
        this.score = 0;
    }
}
