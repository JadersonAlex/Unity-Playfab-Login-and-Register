using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayFabLoginScript : MonoBehaviour {

    public InputField regUsername, regEmail, regPassword;
    public GameObject regPanel;
    public InputField loginUsername, loginPassword;
    public GameObject loginPanel;
    public GameObject userInputPanel;
    public GameObject infoPanel;
    public Text infoUsername, infoCreatedAt;
    public Text errorText;
    public GameObject ResetPasswordPanel;
    public InputField requestedEmail;
    public float time = 3f;

	

    private void OnLoginSuccess(LoginResult obj)
    {
        Debug.Log("Login Succesful!!!");
    }

    
    // Registration
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest();
        request.TitleId = PlayFabSettings.TitleId;

        // Assigning input for the text
        request.Email = regEmail.text;
        request.Username = regUsername.text;
        request.Password = regPassword.text;

        // Submit the registration request to PlayFab API
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterResult, OnPlayFabError);
    }


    // OnPLayFabError will display any errors during registration or log in or anything actually.
    private void OnPlayFabError(PlayFabError obj)
    {
        
        print("Error: " + obj.Error);
        

        string output = "";

        switch(obj.Error)
        {
            case PlayFabErrorCode.AccountBanned:
                output = "Account is banned";
                Invoke("Hide", time);
                break;
            case PlayFabErrorCode.EmailAddressNotAvailable:
                output = "Email is taken";
                Invoke("Hide", time);
                break;
            case PlayFabErrorCode.InvalidParams:
                output = "Invalid Parameters";
                Invoke("Hide", time);
                break;
            case PlayFabErrorCode.InvalidUsernameOrPassword:
                output = "Invalid Username or Password";
                Invoke("Hide", time);
                break;
            case PlayFabErrorCode.UsernameNotAvailable:
                output = "Username is not available";
                Invoke("Hide", time);
                break;
            case PlayFabErrorCode.AccountNotFound:
                output = "Account not found!!";
                Invoke("Hide", time);
                break;
            default:
                break;
        }

        // Assign text for the error output.
        errorText.text = output;
    }

    void Hide(){
	errorText.text = " ";
    }

    private void OnRegisterResult(RegisterPlayFabUserResult obj)
    {
        print("Registration is succesful");

        // Hiding the panel registration
        userInputPanel.SetActive(false);
    }

    public void Login()
    {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = loginUsername.text;
        request.Password = loginPassword.text;

        // Sent request to PlayFab
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginResult, OnPlayFabError);
    }

    private void OnLoginResult(LoginResult obj)
    {
        print("Login Succesful");
        // Deactivate the login panel.

        userInputPanel.SetActive(false);

        // GetAccount Information 
        GetAccountInfo();
    }

    // Account Informationen
    public void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest();
        // Send request to PlayfabAPI
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnPlayFabError);
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult resultData)
    {
        // If infopanel is disabled then enable it ,it's like toggle
        if(!infoPanel.activeSelf)
        {
            infoPanel.SetActive(true);
        }

        print("Account Created Date.");

        // Data output
        print(resultData.AccountInfo.Username);
        // Display result on text
        infoUsername.text = resultData.AccountInfo.Username.ToString();
       
        infoCreatedAt.text = resultData.AccountInfo.PrivateInfo.Email.ToString();
        print(resultData.AccountInfo.Created);
    }

     // If ChangePassword is disabled then enable it ,it's like toggle
    public void ChangePasswordResetPanel(){
    	if(ResetPasswordPanel.activeSelf) {
    		ResetPasswordPanel.SetActive(false);
    	}else{
    		ResetPasswordPanel.SetActive(true);
    	}
    }

    public void RequestPassword(){
    string text = requestedEmail.text;
	    if(text != "")
	    {
	    	var request = new SendAccountRecoveryEmailRequest();
	    	request.Email = text;
	    	request.TitleId = "9F65";
	    	PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoveryEmailSuccess, OnPlayFabError);
	    }
    }

    public void OnRecoveryEmailSuccess(SendAccountRecoveryEmailResult obj)
    {
    print("Your password is in the reset progress, check your email");
    }

}