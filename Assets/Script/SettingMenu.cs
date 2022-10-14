using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer master;
    public AudioMixerGroup menu;
    public AudioMixerGroup player;
    public AudioMixerGroup game;

    public void SetMaster(float volume)
    {
        master.SetFloat("master", volume);
    }

    public void SetMenu(float volume)
    {
        menu.audioMixer.SetFloat("menu", volume);
    }


    public void SetPlayer(float volume)
    {
        player.audioMixer.SetFloat("player", volume);
    }

    public void SetGame(float volume)
    {
        game.audioMixer.SetFloat("game", volume);
    }
}
