using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	//player stats
	float PayPerHour = 7.22f;
	float savings = 5;
	float earnings;
	float careRating; // care rating given by patients - rating from 0-10
	float totalEarned;
	float maxWages = 15; // max wages the player can earn

	int oldAverage;
	int average;
	int warning;

	bool end;
	bool isWarned;

	GameObject dayTimer;

	int[] AllCareRatings = new int[6];

	public void Start() {
		dayTimer = GameObject.Find("DayTimer");
	}

	public string stats() {
		averageRating(); // calculate the average
		totalEarned = savings + earnings; // add up total earnings for the day

		string str = "Player Performance\n\n" + "Savings: £" + savings + "\nEarnings: £" + earnings + "\nPay per hour: £" + PayPerHour +
			"\nTotal earned: £" + totalEarned + "\nAverage rating: " + average + "\nWarning: " + warning;

		// if return true, give notice that player has earned new wages
		if (adjustPay()) {
			str += "\n\nNew Pay per hour: £" + PayPerHour;
		}

		// display info that player has been given a warning
		// only warn once - prevent warning increasing infinitely
		if (isWarned == false && giveWarning()) {
			str += "\n\nYou have recieved a warning.\nYour current warnings now: " + warning;
			isWarned = true;
		}

		// if 3 warnings, display gamer over screen
		if (warning >= 3) end = true;

		return str;
	}

	public void addWage() {
		earnings += PayPerHour; // add pay per hour
	}

	public bool adjustPay() {
		// if current average same as old average, don't change pay and return false
		if (average.Equals(oldAverage)) return false;
		else if (average > 0 && average <= 2){
			PayPerHour = 7.22f;
		}
		else if (average > 2 && average <= 5){
			PayPerHour = 8.70f;
		}
		else if (average > 5 && average <= 7){
			PayPerHour = 10.64f;
		}
		else if (average > 7 && average <= 9){
			PayPerHour = 13.25f;
		}
		else if(average > 9 && average <= 10){ 
			PayPerHour = maxWages;
		}
		return true;
	}

	public float getCurrentMoney() {
		return earnings;
	}

	// add ratings from patients to list
	// index from 0-6 will represent each patient's rating of player
	public void addRating(int index, int rating) {
		AllCareRatings.SetValue(rating, index);
	}

	public void clearRating() {
		for (int i = 0; i < AllCareRatings.Length; i++) AllCareRatings[i] = 0;
	}

	// find the average rating
	public void averageRating() {
		int sum = 0;
		oldAverage = average; // save previous average

		foreach (int num in AllCareRatings){
			sum += num;
		}
		// if rating list not empty, calculate average
		if(!AllCareRatings.Length.Equals(0)) {
			// first day only have 3 patients!
			if (dayTimer.GetComponent<TimerController>().getDay().Equals(1)) average = sum / 3;
			else average = sum / AllCareRatings.Length;
		}
	}

	public bool isEnd(){
		return end;
	}

	public void setIsWarn(bool type) {
		isWarned = type;
	}

	bool giveWarning(){
		// if rating is between 1 and 2; give a warning to the player
		// but not apply this if first day
		if (!dayTimer.GetComponent<TimerController>().getDay().Equals(1) &&
		    average >= 0 && average <= 2){
			if(warning < 3) warning++; // only increase if not greater than 3 - stop
			return true;
		}
		return false;	
	}

	// used in conditionManager - consequence function
	public void setWarning(int num) {
		warning += num;
	}
}
