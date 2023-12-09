using System;
using Facebook.Unity;
using UnityEngine;

public class FacebookManager : PermanentMonoSingleton<FacebookManager>
{
    [SerializeField] private FacebookAuth _facebookAuth;

    protected override void Awake()
    {
        base.Awake();

        LoadComponents();
        InitalizeFacebook();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _facebookAuth = GetComponentInChildren<FacebookAuth>();
    }

    private void InitalizeFacebook()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("(Facebook) Initializing Facebook...");
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("(Facebook) Initialize Facebook Successfull");
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("(Facebook) Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LoginFacebook()
    {
        _facebookAuth.LoginFacebook();
    }

    public string GetUsername()
    {
        return _facebookAuth.GetUsername();
    }
}