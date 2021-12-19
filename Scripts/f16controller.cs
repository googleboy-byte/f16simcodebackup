using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class f16controller : MonoBehaviour
{
    
    [SerializeField]
    private float throttle;
    public Slider throttleslider;
    [SerializeField]
    private float currentenginepower;
    private Rigidbody rb;
    [SerializeField]
    private float maxenginepower = 600f;
    [SerializeField]
    private float currentthrottlevalue;
    [SerializeField]
    private float Enginemaxforce = 20f;
    private Vector3 finalforce;
    private float currentdrag;
    [SerializeField]
    private float currentheightfromterrain;
    [SerializeField]
    private Transform tirefront;
    private bool isgrounded;
    private WheelCollider fwheelcollider;
    private float geardistancefromground = 7f;
    private float yHeight;
    private float yawrate;
    [SerializeField]
    private float maxLiftPower = 0.15f;
    [SerializeField]
    private float liftPower;
    [SerializeField]
    private float turnRate = 100f;
    private Vector3 finalLiftForce;
    [SerializeField]
    private float currentairspeed;
    private RaycastHit hit = new RaycastHit();
    [SerializeField]
    private Camera[] viewcams;
    [SerializeField]
    private float currentflap;
    [SerializeField]
    private Transform rudder, leftaileron, rightaileron, leftelevator, rightelevator;
    private bool rollingright, rollingleft, pitchingup, pitchingdown, yawingright, yawingleft;
    [SerializeField]
    private int currentcamera;
    private int camcount = 0;
    [SerializeField]
    private Slider flapslider;
    [SerializeField]
    private bool gearup;
    [SerializeField]
    private Transform leftgear, rightgear, frontgear, leftgeardoor, rightgeardoor, frontgeardoor;
    [SerializeField]
    private Transform arhorhud, arhor, machpointer, altpointer, throttlecockpit, controlstickcockpit, landinggearlever, nozpospointer, rpmmeterpointer;
    private Quaternion landinggearleverup, landinggearleverdown;
    [SerializeField]
    private TextMeshPro hudspeed, hudaltitude, machval, machvalhud, gearstatehud, hudheadingval, throttlevalhud;
    [SerializeField]
    private GameObject frontgearlight, leftgearlight, rightgearlight;
    [SerializeField]
    private float heading, headingcorrection;
    private bool taximode;
    [SerializeField]
    private LayerMask selectableinstrument;
    [SerializeField]
    private LayerMask terrainmissionmap;
    [SerializeField]
    private Material hightlightmaterial;
    private Collider _selection;
    private float fpcamdist;
    [SerializeField]
    private bool mainpoweron;
    [SerializeField]
    private Transform powerswitch, parkingbreakswitch;
    private bool jetfuelstart1;
    private bool jetfuelstart2;
    private bool jetfueloff;
    [SerializeField]
    private Transform jfs;
    [SerializeField]
    private GameObject hudcomps;
    [SerializeField]
    private float currentfuelleft;
    [SerializeField]
    private float totalfuel;
    private float rpmangle;
    [SerializeField]
    private float currentfuelpercentage;
    [SerializeField]
    private bool parkingbreak;
    [SerializeField]
    private bool throttleidle;
    [SerializeField]
    private bool throttlezero;
    [SerializeField]
    private Transform hudarhordottedup1, hudarhordottedup2, hudarhordotteddown1, hudarhordotteddown2, hudarhorsolidup1, hudarhorsolidup2, hudarhorsoliddown1, hudarhorsoliddown2;
    [SerializeField]
    private Transform masterarmswitch, hudaltswitch, altrdrpowerswitch, fcrpowerswitch, hudfpmattswitch, altladder, hudairspeedswitch;
    [SerializeField]
    private TextMeshPro hudarm;
    private int masterarmstate;
    private int hudaltstate;
    private bool altrdrstate;
    private int firecontrolradarstate;
    [SerializeField]
    private TextMeshPro speedladder1, speedladder2, speedladder3, speedladder4, altladder1, altladder2, altladder3, altladder4, hudpitch11, hudpitch12, hudpitch21, hudpitch22, hudpitch31, hudpitch32, hudpitch41, hudpitch42;
    private int hudfpmattswitchpos, hudairspeedswitchpos; 
    [SerializeField]
    private Light[] consolefloodlights;
    [SerializeField]
    private Transform consolefloodlightsrotary, consoleprilightsrotary;
    private float consolefloodlightsrotaryxrotation, priinstlightsrotaryxrotation, priconsolelightrotaryxrot;
    [SerializeField]
    private Light[] priinstlights;
    [SerializeField]
    private Transform priinstlightsrotary;
    private float speedofsound = 661.4788f;
    private float TAS;
    private float CAS;
    private float currentmach;
    //FUEL
    [SerializeField]
    private float A1, F1F2, AFTRSVR, FWDRSVR, RightInterior, LeftInterior, RightExterior, LeftExterior, CenterlineTank;
    private bool lefthpt, righthpt;
    [SerializeField]
    private Transform lefthptpowerswitch, righthptpowerswitch;
    private float fuelflowinpph;
    [SerializeField]
    private TextMeshPro cockpitfuelflow;
    private bool fueltranswingfirst;
    [SerializeField]
    private Transform extfueltransswitch;
    private int fuelgaugedispstate;
    [SerializeField]
    private Transform fuelgaugepointer1, fuelgaugepointer2, fuelgaugemoderotary;
    [SerializeField]
    private TextMeshPro fuelindicatortotfuelleft;
    [SerializeField]
    private Transform cockpitcompasspointer;
    [SerializeField]
    private TextMeshPro[] cockpittmps;
    [SerializeField]
    private Transform hourhand, minutehand, secondhand;
    private string oldseconds;
    [SerializeField]
    private Transform leftpedal, rightpedal;
    [SerializeField]
    private Light taxilight, landinglight;
    private int gearlightswitchstate;
    [SerializeField]
    private Transform gearlightswitch;
    [SerializeField]
    private Light[] wtlights, flagelights, refuelinglights, flights, anticollisionlights;
    [SerializeField]
    private Transform wtbrtswitch, flagebrtswitch, poslightfsswitch, acollrot, flightrot, masterlightrot, arfuelingrot;
    private int wtbrtswitchstate, flagebrtswitchstate, poslightfsswitchstate, masterlightrotstate;
    private float flightrotstate, arfuelingrotstate;
    private float aclightblinkdelaytimeelapsed, delay;
    private bool aclightisblinking;
    [SerializeField]
    private int acollrotstate;
    [SerializeField]
    private Transform icphudbrtrot, icphudsymrot, icphudcontrot;
    [SerializeField]
    private float icphudbrtrotval, icphudsymrotval, icphudcontrotval, hudbrtness;
    [SerializeField]
    private SpriteRenderer[] hudcompssprites;
    [SerializeField]
    private TextMeshPro[] hudtmps;
    private string currentdedpage;
    [SerializeField]
    private TextMeshPro dedcniuhfval, dedcnivhfval, dedcnistptval, dedcniiffstat1, dedcniiffstat2, dedcnitacanval, dedcnitime;
    [SerializeField]
    private TextMeshPro dedcniuhfvalhud, dedcnivhfvalhud, dedcnistptvalhud, dedcniiffstat1hud, dedcniiffstat2hud, dedcnitacanvalhud, dedcnitimehud;
    private float currentuhf, currentvhf, currentbingo;
    private int currentstpt;
    private int currenticpkpadkey;
    [SerializeField]
    private GameObject[] dedpages;
    [SerializeField]
    private GameObject[] dedpageshud;
    private int icpkpadval;
    private bool icpentrpressed = false;
    private string uhffreqval, vhffreqval, bingofuel, magvmode1, magvval1, magvdir1, egifiltermode1;
    [SerializeField]
    private TextMeshPro uhffreqvaldedset, vhffreqvaldedset, uhffreqvaldedset1, vhffreqvaldedset1, bingoset, bingocurrentfuelleft, magvmode, magvval, magvdir, egifiltermode;
    [SerializeField]
    private TextMeshPro uhffreqvaldedsethud, vhffreqvaldedsethud, uhffreqvaldedset1hud, vhffreqvaldedset1hud, bingosethud, bingocurrentfuellefthud, magvmodehud, magvvalhud, magvdirhud, egifiltermodehud;
    [SerializeField]
    private AudioClip bingosound, engine1, engine2,jfsstartsnd, jfsloopsnd;
    [SerializeField]
    private AudioSource cockpitvoice, enginesoundinscockpit;
    private bool warnsilence, icprtn, icpseq, icpleftup, icpleftdown, icprtnsequp, icprtnseqdown;
    [SerializeField]
    private Transform icprtnswitch;
    private bool ufcpoweron, mfdpoweron, mmcpoweron, dlpoweron, gpspoweron, ststapoweron;
    [SerializeField]
    private Transform ufcpowerswitch, mfdpowerswitch, mmcpowerswitch, dlpowerswitch, gpspowerswitch, ststapowerswitch;
    private int hackhour, hackmin, hacksec;
    private string hacktimetpage, timepageselection;
    [SerializeField]
    private TextMeshPro tpagehackstar, tpagetosstar, tpagedatestar, hacktimetmptpage, hacktimetmpcnipage, hacktimetmptpagehud, hacktimetmpcnipagehud;
    private bool hacktimerrunning;
    private int tpghtimecrsr;
    private bool rpm60orabove;
    private string mfd1mode, mfd2mode;
    [SerializeField]
    private GameObject[] mfd1screens;
    [SerializeField]
    private GameObject[] mfd2screens;
    private int mfd1osb, mfd2osb;
    private string mastermode;
    private string currentaawpn;
    private string currentagwpn;
    [SerializeField]
    private TextMeshPro aawpnselmfd1, aawpnselmfd2;
    public Camera missioncam;
    [SerializeField]
    public List<float> waypointx = new List<float>();
    [SerializeField]
    public List<float> waypointz = new List<float>();
    private float newWayPointx;
    private float newWayPointz;
    private string currentstptmode;
    [SerializeField]
    private TextMeshPro stptpagemode, stptpagestptval, hudcurrentstpt, huddist2currentstpt;
    [SerializeField]
    private TextMeshPro stptpagemodehud, stptpagestptvalhud;
    private float distancetostpt;
    [SerializeField]
    private Transform hudsteeringcue;
    [SerializeField]
    private Transform mfd1hsdcenter;
    [SerializeField]
    private float mapx, mapz;
    [SerializeField]
    private GameObject[] waypointmfd1marker, currentwaypointmfd1marker;
    private float hsdscale;
    [SerializeField]
    private Transform mfd1hsdcenter1, mfd1hsdcenter2;
    [SerializeField]
    private GameObject[] waypointmfd2marker, currentwaypointmfd2marker;
    private float hsdscale2;
    [SerializeField]
    private Transform mfd2hsdcenter1, mfd2hsdcenter2;
    private float angle123;
    [SerializeField]
    private Transform terr;
    [SerializeField]
    private Button removewaypointbtn;
    [SerializeField]
    private Transform ilsheadingind, ilsmeterarrow, ilsmeterarrowmiddle;
    private float ilstcncrs;
    [SerializeField]
    private TextMeshPro hsicrstext, hsirangetext;
    private string hsimode;
    private bool hsicrsrotmouseup;
    [SerializeField]
    private Button selectrnwcrdnts;
    private bool selectingrunway;
    [SerializeField]
    private float rnwx1, rnwx2, rnwz1, rnwz2;
    private int runwayselectclickcount;
    private string currentagmode;
    [SerializeField]
    private TextMeshPro mfd1agccipfuzetext, mfd1agccipwpncnttext, mfd1agccipwpnstatustext, mfd1agccipsysstatustext,
                        mfd1agccipprofiletext, mfd1agccipreleasemodetext,
                        mfd1agccipreleaseintervaldisttext, mfd1agccipreleasepulserequestedtext;
    private string mfd1agccipfuze, mfd1agccipwpncnt, mfd1agccipwpnstatus, mfd1agccipsysstatus,
                        mfd1agccipprofile, mfd1agccipreleasemode,
                        mfd1agccipreleaseintervaldist, mfd1agccipreleasepulserequested;
    [SerializeField]
    private TextMeshPro mfd2agccipfuzetext, mfd2agccipwpncnttext, mfd2agccipwpnstatustext, mfd2agccipsysstatustext,
                        mfd2agccipprofiletext, mfd2agccipreleasemodetext,
                        mfd2agccipreleaseintervaldisttext, mfd2agccipreleasepulserequestedtext;
    private string mfd2agccipfuze, mfd2agccipwpncnt, mfd2agccipwpnstatus, mfd2agccipsysstatus,
                        mfd2agccipprofile, mfd2agccipreleasemode,
                        mfd2agccipreleaseintervaldist, mfd2agccipreleasepulserequested;
    private int gbu87cnt = 4;
    private string lastmfd1mode, lastmfd2mode;
    private string mfddgtentrytopic, mfddgtentryunit;
    [SerializeField]
    private TextMeshPro mfd1dgtentryunittext, mfd1dgtentrytopictext, mfd1dgtentryvaltext;
    [SerializeField]
    private TextMeshPro mfd2dgtentryunittext, mfd2dgtentrytopictext, mfd2dgtentryvaltext;    
    private int mfddgtentryenteredval;
    private bool mfddgtentered;
    private string mfddgtentryid;
    private string mfddgtentryvalstored;
    private float agccipreleaseintervaldistval, agccipreleasepulserequestedval;
    private string sta11, sta12, sta13, sta21, sta22, sta31, sta32, sta33, sta34, sta41, sta42, sta43, sta44, sta51, sta52;
    [SerializeField]
    private GameObject blnkmfd1sta11, blnkmfd1sta12, blnkmfd1sta13, blnkmfd1sta21, blnkmfd1sta22, blnkmfd1sta31, blnkmfd1sta32, blnkmfd1sta33, blnkmfd1sta34, blnkmfd1sta41, blnkmfd1sta42, blnkmfd1sta43, blnkmfd1sta44, blnkmfd1sta51, blnkmfd1sta52;
    [SerializeField]
    private GameObject blnkmfd2sta11, blnkmfd2sta12, blnkmfd2sta13, blnkmfd2sta21, blnkmfd2sta22, blnkmfd2sta31, blnkmfd2sta32, blnkmfd2sta33, blnkmfd2sta34, blnkmfd2sta41, blnkmfd2sta42, blnkmfd2sta43, blnkmfd2sta44, blnkmfd2sta51, blnkmfd2sta52; 
    [SerializeField]
    private GameObject ftank1, ftank2, gbu871, gbu872, gbu873, gbu874, aim9x1, aim9x2, aim1201, aim1202, aim1203, aim1204;
    private int ftankcnt, aim9xcnt, aim120cnt;
    [SerializeField]
    private TextMeshPro mfd1wpntxtsta11, mfd1wpntxtsta12, mfd1wpntxtsta13, mfd1wpntxtsta21, mfd1wpntxtsta22, mfd1wpntxtsta31, mfd1wpntxtsta32, mfd1wpntxtsta33, mfd1wpntxtsta34, mfd1wpntxtsta41, mfd1wpntxtsta42, mfd1wpntxtsta43, mfd1wpntxtsta44, mfd1wpntxtsta51, mfd1wpntxtsta52;
    [SerializeField]
    private TextMeshPro mfd2wpntxtsta11, mfd2wpntxtsta12, mfd2wpntxtsta13, mfd2wpntxtsta21, mfd2wpntxtsta22, mfd2wpntxtsta31, mfd2wpntxtsta32, mfd2wpntxtsta33, mfd2wpntxtsta34, mfd2wpntxtsta41, mfd2wpntxtsta42, mfd2wpntxtsta43, mfd2wpntxtsta44, mfd2wpntxtsta51, mfd2wpntxtsta52;    
    [SerializeField]
    private GameObject gomfd1wpntxtsta11, gomfd1wpntxtsta12, gomfd1wpntxtsta13, gomfd1wpntxtsta21, gomfd1wpntxtsta22, gomfd1wpntxtsta31, gomfd1wpntxtsta32, gomfd1wpntxtsta33, gomfd1wpntxtsta34, gomfd1wpntxtsta41, gomfd1wpntxtsta42, gomfd1wpntxtsta43, gomfd1wpntxtsta44, gomfd1wpntxtsta51, gomfd1wpntxtsta52;
    [SerializeField]
    private GameObject gomfd2wpntxtsta11, gomfd2wpntxtsta12, gomfd2wpntxtsta13, gomfd2wpntxtsta21, gomfd2wpntxtsta22, gomfd2wpntxtsta31, gomfd2wpntxtsta32, gomfd2wpntxtsta33, gomfd2wpntxtsta34, gomfd2wpntxtsta41, gomfd2wpntxtsta42, gomfd2wpntxtsta43, gomfd2wpntxtsta44, gomfd2wpntxtsta51, gomfd2wpntxtsta52;
    private bool jfsstartplayed;
    [SerializeField]
    private bool dedhudshow;
    private int huddedswitchstate;
    [SerializeField]
    private Transform huddedswitchtrans;
    [SerializeField]
    private Transform burner;
    [SerializeField]
    private ParticleSystem burnerpsys;

    void Start(){
        throttle = 0f;
        currentenginepower = 0f;
        rb = this.GetComponent<Rigidbody>();
        currentthrottlevalue = 0f;
        isgrounded = false;
        foreach (Camera cam in viewcams){
            cam.enabled = false;
            camcount += 1;
        }
        missioncam.enabled = false;
        viewcams[0].enabled = true;
        currentcamera = 0;
        flapslider.value = 100f;
        gearup = false;
        taximode = true;
        mainpoweron = false;
        jetfuelstart1 = false;
        jetfuelstart2 = false;
        jetfueloff = true;
        parkingbreak = true;
        throttlezero = true;
        throttleidle = false;
        masterarmstate = 0;
        hudarm.text = " ".ToString();
        hudaltstate = 0;
        altrdrstate = false;
        firecontrolradarstate = 0;
        hudfpmattswitchpos = 0;
        altladder.gameObject.SetActive(false);
        consolefloodlightsrotaryxrotation = 0f;
        hudairspeedswitchpos = 0;
        lefthpt = false;
        righthpt = false;
        fueltranswingfirst = false;
        totalfuel = CenterlineTank + A1 + F1F2 + AFTRSVR + FWDRSVR + RightInterior + LeftInterior + RightExterior + LeftExterior;
        fuelgaugedispstate = 1;
        priconsolelightrotaryxrot = 0f;
        foreach ( Light formlight in flights){
            formlight.intensity = 0f;
        }
        foreach ( Light wtlight in wtlights){
            wtlight.intensity = 0f;
        }
        foreach ( Light flagelight in flagelights){
            flagelight.intensity = 0f;
        }
        foreach ( Light refuellinglight in refuelinglights){
            refuellinglight.intensity = 0f;
        }
        landinglight.intensity = 0f;
        taxilight.intensity = 0f;
        gearlightswitchstate = 0;
        poslightfsswitchstate = 0;
        flagebrtswitchstate = 0;
        wtbrtswitchstate = 0;
        masterlightrotstate = 0;
        arfuelingrotstate = 0;
        acollrotstate = 0;
        flightrotstate = 0f;
        aclightisblinking = false;
        foreach(Light acolllight in anticollisionlights){
            acolllight.enabled = false;
        }
        icphudbrtrotval = 0f;
        icphudsymrotval = 0f;
        icphudcontrotval = 0f;
        currentdedpage = "cni";
        icpkpadval = 11;
        icpentrpressed = false;
        warnsilence = false;
        icprtn = false;
        icpseq = false;
        magvmode1 = "AUTO";
        magvdir1 = "E";
        icpleftup = false;
        icpleftdown = false;
        egifiltermode1 = "AUTO";
        ufcpoweron = false;
        mfdpoweron = false;
        mmcpoweron = false;
        dlpoweron = false;
        gpspoweron = false;
        ststapoweron = false;
        icprtnsequp = false; 
        icprtnseqdown = false;
        timepageselection = "hack";
        tpagehackstar.gameObject.SetActive(false);
        tpagetosstar.gameObject.SetActive(false); 
        tpagedatestar.gameObject.SetActive(false);
        hacktimerrunning = false;
        hacktimetpage = "00:00:00";
        hacktimetmptpage.text = hacktimetpage;
        hacktimetmpcnipage.text = hacktimetpage;
        tpghtimecrsr = 1;
        rpmangle = -90;
        rpm60orabove = false;
        mfd1mode = "home";
        mfd2mode = "home";
        mfd1osb = 21; 
        mfd2osb = 21;
        mastermode = "aa";
        currentagwpn = "gbu87";
        currentagmode = "ccip";
        currentaawpn = "aim9";
        aawpnselmfd1.text = currentaawpn;
        aawpnselmfd2.text = currentaawpn;
        currentstpt = 0;
        currentstptmode = "AUTO";
        foreach(GameObject wpmarker in waypointmfd1marker){
            wpmarker.SetActive(false);
        }
        foreach(GameObject wpmarker in currentwaypointmfd1marker){
            wpmarker.SetActive(false);
        }
        hsdscale = 1f/mapx;
        hsdscale2 = 1f/mapx;
        removewaypointbtn.onClick.AddListener(Removewaypointclicked);
        selectrnwcrdnts.onClick.AddListener(selectrunway);
        ilstcncrs = 0f;
        hsimode = "ils";
        hsicrsrotmouseup = true;
        selectingrunway = false;
        runwayselectclickcount = 0;
        mfd1agccipfuze = "NSTL";
        mfd1agccipwpncnt = gbu87cnt.ToString();
        mfd1agccipwpnstatus = "RDY";
        mfd1agccipsysstatus = "RDY";
        mfd1agccipprofile = "1";
        mfd1agccipreleasemode = "SGL";
        mfd1agccipreleaseintervaldist = "10";
        mfd1agccipreleasepulserequested = "1";
        mfd2agccipfuze = "NSTL";
        mfd2agccipwpncnt = gbu87cnt.ToString();
        mfd2agccipwpnstatus = "RDY";
        mfd2agccipsysstatus = "RDY";
        mfd2agccipprofile = "1";
        mfd2agccipreleasemode = "SGL";
        mfd2agccipreleaseintervaldist = "10";
        mfd2agccipreleasepulserequested = "1";
        mfddgtentryenteredval = 11;
        mfddgtentered = false;
        mfd1dgtentryvaltext.text = "";
        mfd2dgtentryvaltext.text = "";
        mfd1agccipreleasepulserequested = "1";
        mfd2agccipreleasepulserequested = "1";
        ftankcnt = 0;
        if(ftank1.active == true){
            ftankcnt += 1;
        }
        if(ftank2.active == true){
            ftankcnt += 1;
        }
        aim9xcnt = 0;
        if(aim9x1.active == true){
            aim9xcnt += 1;
        }
        if(aim9x2.active == true){
            aim9xcnt += 1;
        }
        aim120cnt = 0;
        if(aim1201.active == true){
            aim120cnt += 1;
        }
        if(aim1202.active == true){
            aim120cnt += 1;
        }
        if(aim1203.active == true){
            aim120cnt += 1;
        }
        if(aim1204.active == true){
            aim120cnt += 1;
        }
        jfsstartplayed = false;
        dedhudshow = false;
        huddedswitchstate = 0;
    
    }

    void Update(){
        CheckFPpos();
        CheckThrottle();
        EngineForce();
        getHeight();
        DragForce();
        GroundControls();
        Yawcontrol();
        CheckLift();
        CheckRoll();
        CheckPitch();
        CheckFlap();
        CheckControlSurfaces();
        CamControl();
        CheckGear();
        CheckHud();
        CheckcockpitLights();
        CheckcockpitInputs();
        CheckInstrumentSelection();
        CheckFuel();
        CheckStall();
        CheckFlybycam();
        CheckDed();
        CheckMFD1();
        CheckMFD2();
        CheckMissionCam();
        CheckHSI();
        CheckStations();
        CheckEngineSound();
        CheckBurnerParticle();

        if (Input.GetAxis("Horizontal") > 0f){
            rollingright = true;
            rollingleft = false;
        }
        if (Input.GetAxis("Horizontal") < 0f){
            rollingright = false;
            rollingleft = true;
        }

        if (Input.GetAxis("Vertical") > 0f){
            pitchingdown = true;
            pitchingup = false;
        }
        if (Input.GetAxis("Vertical") < 0f){
            pitchingdown = false;
            pitchingup = true;
        }
        if (Input.GetKey(KeyCode.Delete) == false){
            yawingleft = false;
        }
        if (Input.GetKey(KeyCode.PageDown) == false){
            yawingright = false;
        }
        if (Input.GetAxis("Vertical") == 0f){
            pitchingdown = false;
            pitchingup = false;
        }
        if (Input.GetAxis("Horizontal") == 0f){
            rollingright = false;
            rollingleft = false;
        }

        currentairspeed = rb.velocity.magnitude;
        currentmach = currentairspeed/661.4788f;
        TAS = speedofsound * currentmach;
        

        if (Input.GetKey(KeyCode.T)){
            if (taximode == true){
                taximode = false;
            } else {
                taximode = true;
            }
        }

        if (currentcamera == 0 && missioncam.enabled == false){
            if ( Input.GetAxis("Mouse ScrollWheel") > 0){
                viewcams[0].fieldOfView = Mathf.Clamp(viewcams[0].fieldOfView - 5f, 5f, 60f);
            }
            if ( Input.GetAxis("Mouse ScrollWheel") < 0){
                viewcams[0].fieldOfView = Mathf.Clamp(viewcams[0].fieldOfView + 5f, 5f, 60f);
            }
        }
        if (missioncam.enabled == true){
            if ( Input.GetAxis("Mouse ScrollWheel") > 0){
                missioncam.gameObject.GetComponent<Missioncam>().missioncamh = Mathf.Clamp(missioncam.transform.position.y - 3000f, 500f, 200000f);
            }
            if ( Input.GetAxis("Mouse ScrollWheel") < 0){
                missioncam.gameObject.GetComponent<Missioncam>().missioncamh = Mathf.Clamp(missioncam.transform.position.y + 3000f, 500f, 200000f);
            }
        }

        string seconds = System.DateTime.UtcNow.ToString("ss");
        if ( seconds != oldseconds ){
            CheckcockpitClock();
        }
        oldseconds = seconds;

        if (yawingleft == true){
            leftpedal.localRotation = Quaternion.Euler(8.307f, -88.144f, -40.799f);
        } else if (yawingleft == false){
            leftpedal.localRotation = Quaternion.Euler(8.307f, -88.144f, -56.628f);
        }
        if (yawingright == true){
            rightpedal.localRotation = Quaternion.Euler(-5.731f, -92.36f, -30.167f);
        } else if (yawingright == false){
            rightpedal.localRotation = Quaternion.Euler(-5.731f, -92.36f, -47.573f);
        }

        icpkpadval = 11;
        icpentrpressed = false;

        if(mainpoweron == true && jetfueloff == false && cockpitvoice.isPlaying == false && currentbingo >= currentfuelleft && warnsilence == false){
            cockpitvoice.clip = bingosound;
            cockpitvoice.volume = 1f;
            cockpitvoice.Play();
        }
        if (icprtn == true){
            icprtnswitch.localRotation = Quaternion.Euler(37.192f, 10.231f, 0f);
        } else if(icpseq == true){
            icprtnswitch.localRotation = Quaternion.Euler(-10.719f, 10.231f, 0f);
        } else if(icprtnsequp == true){
            icprtnswitch.localRotation = Quaternion.Euler(0f, 32.512f, 0f);
        } else if(icprtnseqdown == true){
            icprtnswitch.localRotation = Quaternion.Euler(0f, -11.061f, 0f);
        } else{
            icprtnswitch.localRotation = Quaternion.Euler(0f, 10.231f, 0f);
        }

        hacktimetpage = hackhour.ToString() + ":" + hackmin.ToString() + ":" + hacksec.ToString();
        hacktimetmptpage.text = hacktimetpage;
        hacktimetmpcnipage.text = hacktimetpage;

        icprtn = false;
        icpseq = false;
        icprtnsequp = false; 
        icprtnseqdown = false;
        icpleftup = false;
        icpleftdown = false; 
        if (magvval1 == ""){
            headingcorrection = 0f;
        } else{
            if (magvdir1 == "E"){
                headingcorrection = float.Parse(magvval1) * -1f;
            } else if (magvdir1 == "W"){
                headingcorrection = float.Parse(magvval1);
            }
        }
        mfd1osb = 21; 
        mfd2osb = 21;
        
    }

    public float DistanceFromPointToLine(Vector2 a, Vector2 b, Vector2 c)
	{
		float s1 = -b.y + a.y;
		float s2 = b.x - a.x;
		return Mathf.Abs((c.x - a.x) * s1 + (c.y - a.y) * s2) / Mathf.Sqrt(s1*s1 + s2*s2);
	}

    public float SideOfLine(Vector2 a, Vector2 b, Vector2 c)
	{
		return Mathf.Sign((c.x - a.x) * (-b.y + a.y) + (c.y - a.y) * (b.x - a.x));
	}

    void CheckBurnerParticle(){

        if(mainpoweron == true && rpm60orabove == true){
            burner.gameObject.SetActive(true);
        }
        if(mainpoweron == false || rpm60orabove == false){
            burner.gameObject.SetActive(false);
        }
        if(mainpoweron == true){
            float burnerpercent = (throttleslider.value/throttleslider.maxValue) * 100f;
            burner.localScale = new Vector3(burner.localScale.x, burner.localScale.y, Mathf.Clamp(0.009f + (burnerpercent/100f)* 0.041f, 0.009f, 0.05f));
        }

    }

    void CheckStations(){
        sta11 = "blnk";
        sta12 = "blnk";
        sta13 = "blnk";
        if (ftank1.active == true){
            sta21 = "1 TNK";
        } else {
            sta21 = "blnk";
        }
        if (ftank2.active == true){
            sta22 = "1 TNK";
        } else {
            sta22 = "blnk";
        }
        if (gbu871.active == true){
            sta31 = "1 GBU87";
        } else {
            sta31 = "blnk";
        }
        if (gbu872.active == true){
            sta32 = "1 GBU87";
        } else {
            sta32 = "blnk";
        }
        if (gbu873.active == true){
            sta33 = "1 GBU87";
        } else {
            sta33 = "blnk";
        }
        if (gbu874.active == true){
            sta34 = "1 GBU87";
        } else {
            sta34 = "blnk";
        }
        if (aim9x1.active == true){
            sta41 = "1 AIM9";
        } else {
            sta41 = "blnk";
        }
        if (aim9x2.active == true){
            sta43 = "1 AIM9";
        } else {
            sta43 = "blnk";
        }
        if (aim1201.active == true){
            sta42 = "1 AIM120";
        } else {
            sta42 = "blnk";
        }
        if (aim1202.active == true){
            sta44 = "1 AIM120";
        } else {
            sta44 = "blnk";
        }
        if (aim1203.active == true){
            sta51 = "1 AIM120";
        } else {
            sta51 = "blnk";
        }
        if (aim1204.active == true){
            sta52 = "1 AIM120";
        } else {
            sta52 = "blnk";
        }

    }

    void CheckHSI(){
        if(rpm60orabove == true && mainpoweron == true && hsicrsrotmouseup == false){
            ilstcncrs += Input.GetAxis("Mouse Y");
        }
        if(rpm60orabove == true && mainpoweron == true){
            if(hsimode == "ils"){
                ilsheadingind.gameObject.SetActive(true);
            }
        } else{
            ilsheadingind.gameObject.SetActive(false);
        }
        ilsheadingind.localRotation = Quaternion.Euler(0f, heading, 0f);
        ilsmeterarrow.localRotation = Quaternion.Euler(0f, ilstcncrs, 0f);
        hsicrstext.text = ilstcncrs.ToString("F0");
        Vector2 originrnw = new Vector2(rnwx1, rnwz1);
        Vector2 endrnw = new Vector2(rnwx2, rnwz2);
        Vector2 direction123 = (originrnw - endrnw).normalized;
        Vector2 fighterpoint = new Vector2(transform.position.x, transform.position.z);
        hsirangetext.text = (Vector2.Distance(fighterpoint, originrnw)/1000f).ToString("F0");
        //Vector2 rnwlineintersectpoint = FindNearestPointOnLine(originrnw, direction123, fighterpoint);
        //float disttornwalign = Mathf.Sqrt((fighterpoint.x - rnwlineintersectpoint.x) * (fighterpoint.x - rnwlineintersectpoint.x) + (fighterpoint.y - rnwlineintersectpoint.y) * (fighterpoint.y - rnwlineintersectpoint.y));
        float disttornwalign = DistanceFromPointToLine(endrnw, originrnw, fighterpoint);
        if(disttornwalign >= -2f && disttornwalign <= 2f){
            ilsmeterarrowmiddle.localPosition = new Vector3 (-0.05276695f, -0.1144875f, 0.006107873f);
        } else{
            ilsmeterarrowmiddle.localPosition = new Vector3 (-0.05276695f, -0.1144875f, 0.006107873f + (disttornwalign * -0.0001f * SideOfLine(endrnw, originrnw, fighterpoint)));
        }
    }

    void CheckEngineSound(){
        if(enginesoundinscockpit.volume > (throttleslider.value/100f)*0.15f){
            enginesoundinscockpit.volume = Mathf.Clamp(enginesoundinscockpit.volume - 0.07f * Time.deltaTime, 0.05f, 0.15f);
        } else if(enginesoundinscockpit.volume < (throttleslider.value/100f)*0.15f){
            enginesoundinscockpit.volume = Mathf.Clamp(enginesoundinscockpit.volume + 0.07f * Time.deltaTime, 0.05f, 0.15f);
        }
        if(rpmmeterpointer.localRotation.eulerAngles.y >= 15f && rpmmeterpointer.localRotation.eulerAngles.y <= 198.1f && enginesoundinscockpit.isPlaying == false){
            enginesoundinscockpit.clip = engine2;
            enginesoundinscockpit.Play();
        } else if(rpm60orabove == false && mainpoweron == true && enginesoundinscockpit.isPlaying == false && rpmmeterpointer.localRotation.eulerAngles.y < 289f && jfsstartplayed == false){
            if(enginesoundinscockpit.clip != jfsstartsnd){
                enginesoundinscockpit.clip = jfsstartsnd;
            }
            enginesoundinscockpit.volume = 0.01f;
            enginesoundinscockpit.Play();
            jfsstartplayed = true;
        } else if(rpm60orabove == false && mainpoweron == true && enginesoundinscockpit.isPlaying == false){
            enginesoundinscockpit.clip = engine2;
            enginesoundinscockpit.volume = 0.1f;
            enginesoundinscockpit.Play();
        }

    }

    void selectrunway(){
        selectingrunway = true;
    }

    void Removewaypointclicked(){
        waypointx.RemoveAt(waypointx.Count - 1);
        waypointz.RemoveAt(waypointz.Count - 1);
    }

    void CheckFPpos(){
        if(transform.position.z >= 7500f){
            transform.position = new Vector3(transform.position.x, transform.position.y,0f);
            terr.position = new Vector3(terr.position.x, terr.position.y, terr.position.z - 7500f);
            for( int i = 0; i < waypointz.Count; i++ ){
                waypointz[i] -= 7500f;
            }
        } else if(transform.position.z <= -7500f){
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            terr.position = new Vector3(terr.position.x, terr.position.y, terr.position.z + 7500f);
            for( int i = 0; i < waypointz.Count; i++ ){
                waypointz[i] += 7500f;
            }
        }
        if(transform.position.x >= 7500f){
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            terr.position = new Vector3(terr.position.x - 7500f, terr.position.y, terr.position.z);
            for( int i = 0; i < waypointx.Count; i++ ){
                waypointx[i] -= 7500f;
            }
        } else if(transform.position.x <= -7500f){
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            terr.position = new Vector3(terr.position.x + 7500f, terr.position.y, terr.position.z);
            for( int i = 0; i < waypointx.Count; i++ ){
                waypointx[i] += 7500f;
            }
        }
    }

    void CheckMissionCam(){
        if(Input.GetKeyUp(KeyCode.M)){
            if(missioncam.enabled == false){
                foreach (Camera cam in viewcams){
                    cam.enabled = false;
                }
                missioncam.enabled = true;
            } else{
                missioncam.enabled = false;
                viewcams[currentcamera].enabled = true;
            }
        }
        if(missioncam.enabled == true){
            if(Input.GetMouseButtonUp(0)){
                if(EventSystem.current.IsPointerOverGameObject() == false){
                    if(selectingrunway == false){
                        Ray ray12345 = missioncam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit12345;
                        if(Physics.Raycast(ray12345, out hit12345, Mathf.Infinity, terrainmissionmap)){
                            newWayPointx = hit12345.point.x;
                            newWayPointz = hit12345.point.z;
                            waypointx.Add(newWayPointx);
                            waypointz.Add(newWayPointz);
                        }                        
                    } else if(selectingrunway == true){
                        if(runwayselectclickcount >= 2){
                            selectingrunway = false;
                        } else{
                            if(runwayselectclickcount == 0){
                                Ray ray12345 = missioncam.ScreenPointToRay(Input.mousePosition);
                                RaycastHit hit12345;
                                if(Physics.Raycast(ray12345, out hit12345, Mathf.Infinity, terrainmissionmap)){
                                    newWayPointx = hit12345.point.x;
                                    newWayPointz = hit12345.point.z;
                                    rnwx1 = newWayPointx;
                                    rnwz1 = newWayPointz;
                                    runwayselectclickcount = 1;
                                }
                            } else if(runwayselectclickcount == 1){
                                Ray ray12345 = missioncam.ScreenPointToRay(Input.mousePosition);
                                RaycastHit hit12345;
                                if(Physics.Raycast(ray12345, out hit12345, Mathf.Infinity, terrainmissionmap)){
                                    newWayPointx = hit12345.point.x;
                                    newWayPointz = hit12345.point.z;
                                    rnwx2 = newWayPointx;
                                    rnwz2 = newWayPointz;
                                    runwayselectclickcount = 2;
                                    selectingrunway = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        if(waypointx.Count == 1){
            currentstptmode = "MAN";
            currentstpt = 1;
        }
        if(waypointx.Count > 0){
            Vector3 differencebetweenfighterandwpoint = new Vector3(
                transform.position.x - waypointx[currentstpt - 1],
                transform.position.y - transform.position.y,
                transform.position.z - waypointz[currentstpt - 1]);
            distancetostpt = Mathf.Sqrt(
                Mathf.Pow(differencebetweenfighterandwpoint.x, 2f) +
                Mathf.Pow(differencebetweenfighterandwpoint.y, 2f) +
                Mathf.Pow(differencebetweenfighterandwpoint.z, 2f));
            Vector3 relative1234 = transform.InverseTransformPoint(new Vector3(waypointx[currentstpt - 1], transform.position.y, waypointz[currentstpt - 1]));
            angle123 = Mathf.Atan2(relative1234.x, relative1234.z) * Mathf.Rad2Deg;
            hudsteeringcue.localRotation = Quaternion.Euler(0, 0, -angle123);
                // huddist2currentstpt.text = (distancetostpt/1000f).ToString("F1");
        }
    }

    void CheckMFD1(){
        if (mainpoweron == true && mfdpoweron == true && rpm60orabove == true){

            if(mfddgtentered == true){
                if(mfddgtentryid == "IMPACT SPACING"){
                    agccipreleaseintervaldistval = float.Parse(mfddgtentryvalstored);
                    mfd1agccipreleaseintervaldist = agccipreleaseintervaldistval.ToString("F0");
                    mfd2agccipreleaseintervaldist = agccipreleaseintervaldistval.ToString("F0");
                }
                if(mfddgtentryid == "RELEASE PULSES"){
                    agccipreleasepulserequestedval = float.Parse(mfddgtentryvalstored);
                    mfd1agccipreleasepulserequested = agccipreleasepulserequestedval.ToString("F0");
                    mfd2agccipreleasepulserequested = agccipreleasepulserequestedval.ToString("F0");
                }
                mfddgtentered = false;
            }

            if(sta11 != "blnk"){
                blnkmfd1sta11.SetActive(false);
                gomfd1wpntxtsta11.SetActive(true);
                mfd1wpntxtsta11.text = sta11;
            }else{
                blnkmfd1sta11.SetActive(true);
                gomfd1wpntxtsta11.SetActive(false);
            }
            if(sta12 != "blnk"){
                blnkmfd1sta12.SetActive(false);
                gomfd1wpntxtsta12.SetActive(true);
                mfd1wpntxtsta12.text = sta12;
            }else{
                blnkmfd1sta12.SetActive(true);
                gomfd1wpntxtsta12.SetActive(false);
            }
            if(sta13 != "blnk"){
                blnkmfd1sta13.SetActive(false);
                gomfd1wpntxtsta13.SetActive(true);
                mfd1wpntxtsta13.text = sta13;
            }else{
                blnkmfd1sta13.SetActive(true);
                gomfd1wpntxtsta13.SetActive(false);
            }
            if(sta21 != "blnk"){
                blnkmfd1sta21.SetActive(false);
                gomfd1wpntxtsta21.SetActive(true);
                mfd1wpntxtsta21.text = sta21;
            }else{
                blnkmfd1sta21.SetActive(true);
                gomfd1wpntxtsta21.SetActive(false);
            }
            if(sta22 != "blnk"){
                blnkmfd1sta22.SetActive(false);
                gomfd1wpntxtsta22.SetActive(true);
                mfd1wpntxtsta22.text = sta22;
            }else{
                blnkmfd1sta22.SetActive(true);
                gomfd1wpntxtsta22.SetActive(false);
            }
            if(sta31 != "blnk"){
                blnkmfd1sta31.SetActive(false);
                gomfd1wpntxtsta31.SetActive(true);
                mfd1wpntxtsta31.text = sta31;
            }else{
                blnkmfd1sta31.SetActive(true);
                gomfd1wpntxtsta31.SetActive(false);
            }
            if(sta32 != "blnk"){
                blnkmfd1sta32.SetActive(false);
                gomfd1wpntxtsta32.SetActive(true);
                mfd1wpntxtsta32.text = sta32;
            }else{
                blnkmfd1sta32.SetActive(true);
                gomfd1wpntxtsta32.SetActive(false);
            }
            if(sta33 != "blnk"){
                blnkmfd1sta33.SetActive(false);
                gomfd1wpntxtsta33.SetActive(true);
                mfd1wpntxtsta33.text = sta33;
            }else{
                blnkmfd1sta33.SetActive(true);
                gomfd1wpntxtsta33.SetActive(false);
            }
            if(sta34 != "blnk"){
                blnkmfd1sta34.SetActive(false);
                gomfd1wpntxtsta34.SetActive(true);
                mfd1wpntxtsta34.text = sta34;
            }else{
                blnkmfd1sta34.SetActive(true);
                gomfd1wpntxtsta34.SetActive(false);
            }
            if(sta41 != "blnk"){
                blnkmfd1sta41.SetActive(false);
                gomfd1wpntxtsta41.SetActive(true);
                mfd1wpntxtsta41.text = sta41;
            }else{
                blnkmfd1sta41.SetActive(true);
                gomfd1wpntxtsta41.SetActive(false);
            }
            if(sta42 != "blnk"){
                blnkmfd1sta42.SetActive(false);
                gomfd1wpntxtsta42.SetActive(true);
                mfd1wpntxtsta42.text = sta42;
            }else{
                blnkmfd1sta42.SetActive(true);
                gomfd1wpntxtsta42.SetActive(false);
            }
            if(sta43 != "blnk"){
                blnkmfd1sta43.SetActive(false);
                gomfd1wpntxtsta43.SetActive(true);
                mfd1wpntxtsta43.text = sta43;
            }else{
                blnkmfd1sta43.SetActive(true);
                gomfd1wpntxtsta43.SetActive(false);
            }
            if(sta44 != "blnk"){
                blnkmfd1sta44.SetActive(false);
                gomfd1wpntxtsta44.SetActive(true);
                mfd1wpntxtsta44.text = sta44;
            }else{
                blnkmfd1sta44.SetActive(true);
                gomfd1wpntxtsta44.SetActive(false);
            }
            if(sta51 != "blnk"){
                blnkmfd1sta51.SetActive(false);
                gomfd1wpntxtsta51.SetActive(true);
                mfd1wpntxtsta51.text = sta51;
            }else{
                blnkmfd1sta51.SetActive(true);
                gomfd1wpntxtsta51.SetActive(false);
            }
            if(sta52 != "blnk"){
                blnkmfd1sta52.SetActive(false);
                gomfd1wpntxtsta52.SetActive(true);
                mfd1wpntxtsta52.text = sta52;
            }else{
                blnkmfd1sta52.SetActive(true);
                gomfd1wpntxtsta52.SetActive(false);
            }

            if(mfd1mode == "home"){
                foreach(GameObject mfd1screen in mfd1screens){
                    mfd1screen.SetActive(false);
                }
                mfd1screens[0].SetActive(true);
                if(mfd1osb == 6 || mfd1osb == 14){
                    mfd1mode = "smsinv";
                    mfd1osb = 21;
                }
                if(mfd1osb == 13){
                    mfd1mode = "hsd";
                    mfd1osb = 21;
                }
            }
            if(mfd1mode == "smsinv"){
                foreach(GameObject mfd1screen in mfd1screens){
                    mfd1screen.SetActive(false);
                }
                mfd1screens[1].SetActive(true);
                if(mfd1osb == 14){
                    mfd1mode = "home";
                    mfd1osb = 21;
                }
                if(mfd1osb == 4){
                    if(mastermode == "aa"){
                        mfd1mode = "smsaa";
                    } else if(mastermode == "ag"){
                        mfd1mode = "smsag";
                    }
                    mfd1osb = 21;
                }
                if(mfd1osb == 13){
                    mfd1mode = "hsd";
                    mfd1osb = 21;
                }
            }
            if(mfd1mode == "smsaa"){
                foreach(GameObject mfd1screen in mfd1screens){
                    mfd1screen.SetActive(false);
                }
                mfd1screens[2].SetActive(true);
                if(mfd1osb == 7){
                    if(currentaawpn == "aim120"){
                        currentaawpn = "aim9";
                    } else{
                        currentaawpn = "aim120";
                    }
                    mfd1osb = 21;
                }
                if(mfd1osb == 4){
                    mfd1mode = "smsinv";
                    mfd1osb = 21;
                }
                if(mfd1osb == 14){
                    mfd1mode = "home";
                    mfd1osb = 21;
                }
                if(mfd1osb == 13){
                    mfd1mode = "hsd";
                    mfd1osb = 21;
                }
                if(currentaawpn == "aim120"){
                    aawpnselmfd1.text = "4A - 120C";
                } else{
                    aawpnselmfd1.text = "2A - 9LM";
                }
            }
            if(mfd1mode == "smsag"){
                foreach(GameObject mfd1screen in mfd1screens){
                    mfd1screen.SetActive(false);
                }
                mfd1screens[4].SetActive(true);
                if(currentagmode == "ccip"){
                    mfd1agccipfuzetext.text = mfd1agccipfuze;
                    mfd1agccipwpncnttext.text = mfd1agccipwpncnt;
                    mfd1agccipwpnstatustext.text = mfd1agccipwpnstatus;
                    mfd1agccipsysstatustext.text = mfd1agccipsysstatus;
                    mfd1agccipprofiletext.text = mfd1agccipprofile;
                    mfd1agccipreleasemodetext.text = mfd1agccipreleasemode;
                    mfd1agccipreleaseintervaldisttext.text = mfd1agccipreleaseintervaldist;
                    mfd1agccipreleasepulserequestedtext.text = mfd1agccipreleasepulserequested;
                    if(mfd1osb == 8){
                        if(mfd1agccipreleasemode == "SGL"){
                            mfd1agccipreleasemode = "PAIR";
                        } else{
                            mfd1agccipreleasemode = "SGL";
                        }
                        mfd1osb = 21;
                    }
                    if(mfd1osb == 18){
                        if(mfd1agccipfuze == "NSTL"){
                            mfd1agccipfuze = "NOSE";
                        } else if(mfd1agccipfuze == "NOSE"){
                            mfd1agccipfuze = "TAIL";
                        } else if(mfd1agccipfuze == "TAIL"){
                            mfd1agccipfuze = "NSTL";
                        }
                        mfd1osb = 21;
                    }
                    if(mfd1osb == 9){
                        lastmfd1mode = "smsag";
                        mfddgtentrytopic = "IMPACT SPACING";
                        mfddgtentryunit = "FT";
                        mfd1osb = 21;
                        mfd1mode = "dgtentry";
                        mfd1dgtentryvaltext.text = "0";
                        mfd2dgtentryvaltext.text = "0";
                    }
                    if(mfd1osb == 10){
                        lastmfd1mode = "smsag";
                        mfddgtentrytopic = "RELEASE PULSES";
                        mfddgtentryunit = " ";
                        mfd1osb = 21;
                        mfd1mode = "dgtentry";
                        mfd1dgtentryvaltext.text = "0";
                        mfd2dgtentryvaltext.text = "0";
                        
                    }
                }
                if(mfd1osb == 4){
                    mfd1mode = "smsinv";
                    mfd1osb = 21;
                }
                if(mfd1osb == 14){
                    mfd1mode = "home";
                    mfd1osb = 21;
                }
                if(mfd1osb == 13){
                    mfd1mode = "hsd";
                    mfd1osb = 21;
                }
            }
            if(mfd1mode == "hsd"){
                foreach(GameObject mfd1screen in mfd1screens){
                    mfd1screen.SetActive(false);
                }
                mfd1screens[3].SetActive(true);
                for( int i = 0; i < waypointz.Count; i++ ){
                    Vector3 mfd1waypointcrdnate = new Vector3((hsdscale) * (-waypointz[i] + transform.position.z), (hsdscale) * (waypointx[i] - transform.position.x), -0.6859215f);
                    if(i == currentstpt - 1){
                        currentwaypointmfd1marker[i].SetActive(true);
                        waypointmfd1marker[i].SetActive(false);
                        currentwaypointmfd1marker[i].transform.localPosition = mfd1waypointcrdnate;
                        currentwaypointmfd1marker[i].transform.localRotation = transform.rotation;
                    } else{
                        waypointmfd1marker[i].SetActive(true);
                        currentwaypointmfd1marker[i].SetActive(false);
                        waypointmfd1marker[i].transform.localPosition = mfd1waypointcrdnate;
                    }
                }
                for( int i = 0; i < waypointmfd1marker.Length; i++ ){
                    if(i > waypointx.Count - 1){
                        waypointmfd1marker[i].SetActive(false);
                        currentwaypointmfd1marker[i].SetActive(false);
                    }
                }
                //mfd1hsdcenter1.localPosition = new Vector3 (-transform.position.x * hsdscale, -transform.position.z * hsdscale, -0.01f);
                //mfd1hsdcenter2.localPosition = new Vector3 (-transform.position.x * hsdscale, -transform.position.z * hsdscale, -0.01f);
                mfd1hsdcenter1.localRotation = Quaternion.Euler(0f, 0f, -(Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg) - angle123);
                mfd1hsdcenter2.localRotation = Quaternion.Euler(0f, 0f, -(Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg) - angle123);
                if(mfd1osb == 13){
                    mfd1mode = "home";
                    mfd1osb = 21;
                }
                if(mfd1osb == 20){
                    hsdscale += 0.000001f;
                    mfd1osb = 21;
                }
                if(mfd1osb == 19){
                    hsdscale -= 0.000001f;
                    mfd1osb = 21;
                }
                if(mfd1osb == 14){
                    mfd1mode = "smsinv";
                    mfd1osb = 21;
                }
            }
            if(mfd1mode == "dgtentry"){
                foreach(GameObject mfd1screen in mfd1screens){
                    mfd1screen.SetActive(false);
                }
                mfd1screens[5].SetActive(true);
                mfd1dgtentryunittext.text = mfddgtentryunit;
                mfd1dgtentrytopictext.text = mfddgtentrytopic;
                if(mfd1osb == 3){
                    mfd1mode = lastmfd1mode;
                    mfd1dgtentryvaltext.text = "";
                    mfd1osb = 21;
                }
                if(mfd1osb == 2){
                    mfd1mode = lastmfd1mode;
                    mfd1osb = 21;
                    mfddgtentered = true;
                    mfddgtentryid = mfddgtentrytopic;
                    mfddgtentryvalstored = mfd1dgtentryvaltext.text;
                    mfd1dgtentryvaltext.text = "";
                }
                if(mfd1osb != 21){
                    if(mfd1osb == 20){
                        mfddgtentryenteredval = 1;
                    }
                    if(mfd1osb == 19){
                        mfddgtentryenteredval = 2;
                    }
                    if(mfd1osb == 18){
                        mfddgtentryenteredval = 3;
                    }
                    if(mfd1osb == 17){
                        mfddgtentryenteredval = 4;
                    }
                    if(mfd1osb == 16){
                        mfddgtentryenteredval = 5;
                    }
                    if(mfd1osb == 6){
                        mfddgtentryenteredval = 6;
                    }
                    if(mfd1osb == 7){
                        mfddgtentryenteredval = 7;
                    }
                    if(mfd1osb == 8){
                        mfddgtentryenteredval = 8;
                    }
                    if(mfd1osb == 9){
                        mfddgtentryenteredval = 9;
                    }
                    if(mfd1osb == 10){
                        mfddgtentryenteredval = 0;
                    }
                    mfd1osb = 21;
                    mfd1dgtentryvaltext.text += mfddgtentryenteredval.ToString();
                    mfd2dgtentryvaltext.text += mfddgtentryenteredval.ToString();
                }
                mfddgtentryenteredval = 11;
            }
            if(mfd1osb == 15){
                string mfd2mode123 = mfd2mode;
                mfd2mode = mfd1mode;
                mfd1mode = mfd2mode123;
                mfd1osb = 21;
            }
        } else{
            foreach(GameObject mfd1screen in mfd1screens){
                mfd1screen.SetActive(false);
            }
        }
    }
    void CheckMFD2(){
        if (mainpoweron == true && mfdpoweron == true && rpm60orabove == true){

            if(sta11 != "blnk"){
                blnkmfd2sta11.SetActive(false);
                gomfd2wpntxtsta11.SetActive(true);
                mfd2wpntxtsta11.text = sta11;
            }else{
                blnkmfd2sta11.SetActive(true);
                gomfd2wpntxtsta11.SetActive(false);
            }
            if(sta12 != "blnk"){
                blnkmfd2sta12.SetActive(false);
                gomfd2wpntxtsta12.SetActive(true);
                mfd2wpntxtsta12.text = sta12;
            }else{
                blnkmfd2sta12.SetActive(true);
                gomfd2wpntxtsta12.SetActive(false);
            }
            if(sta13 != "blnk"){
                blnkmfd2sta13.SetActive(false);
                gomfd2wpntxtsta13.SetActive(true);
                mfd2wpntxtsta13.text = sta13;
            }else{
                blnkmfd2sta13.SetActive(true);
                gomfd2wpntxtsta13.SetActive(false);
            }
            if(sta21 != "blnk"){
                blnkmfd2sta21.SetActive(false);
                gomfd2wpntxtsta21.SetActive(true);
                mfd2wpntxtsta21.text = sta21;
            }else{
                blnkmfd2sta21.SetActive(true);
                gomfd2wpntxtsta21.SetActive(false);
            }
            if(sta22 != "blnk"){
                blnkmfd2sta22.SetActive(false);
                gomfd2wpntxtsta22.SetActive(true);
                mfd2wpntxtsta22.text = sta22;
            }else{
                blnkmfd2sta22.SetActive(true);
                gomfd2wpntxtsta22.SetActive(false);
            }
            if(sta31 != "blnk"){
                blnkmfd2sta31.SetActive(false);
                gomfd2wpntxtsta31.SetActive(true);
                mfd2wpntxtsta31.text = sta31;
            }else{
                blnkmfd2sta31.SetActive(true);
                gomfd2wpntxtsta31.SetActive(false);
            }
            if(sta32 != "blnk"){
                blnkmfd2sta32.SetActive(false);
                gomfd2wpntxtsta32.SetActive(true);
                mfd2wpntxtsta32.text = sta32;
            }else{
                blnkmfd2sta32.SetActive(true);
                gomfd2wpntxtsta32.SetActive(false);
            }
            if(sta33 != "blnk"){
                blnkmfd2sta33.SetActive(false);
                gomfd2wpntxtsta33.SetActive(true);
                mfd2wpntxtsta33.text = sta33;
            }else{
                blnkmfd2sta33.SetActive(true);
                gomfd2wpntxtsta33.SetActive(false);
            }
            if(sta34 != "blnk"){
                blnkmfd2sta34.SetActive(false);
                gomfd2wpntxtsta34.SetActive(true);
                mfd2wpntxtsta34.text = sta34;
            }else{
                blnkmfd2sta34.SetActive(true);
                gomfd2wpntxtsta34.SetActive(false);
            }
            if(sta41 != "blnk"){
                blnkmfd2sta41.SetActive(false);
                gomfd2wpntxtsta41.SetActive(true);
                mfd2wpntxtsta41.text = sta41;
            }else{
                blnkmfd2sta41.SetActive(true);
                gomfd2wpntxtsta41.SetActive(false);
            }
            if(sta42 != "blnk"){
                blnkmfd2sta42.SetActive(false);
                gomfd2wpntxtsta42.SetActive(true);
                mfd2wpntxtsta42.text = sta42;
            }else{
                blnkmfd2sta42.SetActive(true);
                gomfd2wpntxtsta42.SetActive(false);
            }
            if(sta43 != "blnk"){
                blnkmfd2sta43.SetActive(false);
                gomfd2wpntxtsta43.SetActive(true);
                mfd2wpntxtsta43.text = sta43;
            }else{
                blnkmfd2sta43.SetActive(true);
                gomfd2wpntxtsta43.SetActive(false);
            }
            if(sta44 != "blnk"){
                blnkmfd2sta44.SetActive(false);
                gomfd2wpntxtsta44.SetActive(true);
                mfd2wpntxtsta44.text = sta44;
            }else{
                blnkmfd2sta44.SetActive(true);
                gomfd2wpntxtsta44.SetActive(false);
            }
            if(sta51 != "blnk"){
                blnkmfd2sta51.SetActive(false);
                gomfd2wpntxtsta51.SetActive(true);
                mfd2wpntxtsta51.text = sta51;
            }else{
                blnkmfd2sta51.SetActive(true);
                gomfd2wpntxtsta51.SetActive(false);
            }
            if(sta52 != "blnk"){
                blnkmfd2sta52.SetActive(false);
                gomfd2wpntxtsta52.SetActive(true);
                mfd2wpntxtsta52.text = sta52;
            }else{
                blnkmfd2sta52.SetActive(true);
                gomfd2wpntxtsta52.SetActive(false);
            }

            if(mfd2mode == "home"){
                foreach(GameObject mfd2screen in mfd2screens){
                    mfd2screen.SetActive(false);
                }
                mfd2screens[0].SetActive(true);
                if(mfd2osb == 6 || mfd2osb == 14){
                    mfd2mode = "smsinv";
                    mfd2osb = 21;
                }
                if(mfd2osb == 13){
                    mfd2mode = "hsd";
                    mfd2osb = 21;
                }
            }
            if(mfd2mode == "smsinv"){
                foreach(GameObject mfd2screen in mfd2screens){
                    mfd2screen.SetActive(false);
                }
                mfd2screens[1].SetActive(true);
                if(mfd2osb == 14){
                    mfd2mode = "home";
                    mfd2osb = 21;
                }
                if(mfd2osb == 4){
                    if(mastermode == "aa"){
                        mfd2mode = "smsaa";
                    } else if(mastermode == "ag"){
                        mfd2mode = "smsag";
                    }
                    mfd2osb = 21;
                }
                if(mfd2osb == 14){
                    mfd2mode = "home";
                    mfd2osb = 21;
                }
                if(mfd2osb == 13){
                    mfd2mode = "hsd";
                    mfd2osb = 21;
                }
            }
            if(mfd2mode == "smsaa"){
                foreach(GameObject mfd2screen in mfd2screens){
                    mfd2screen.SetActive(false);
                }
                mfd2screens[2].SetActive(true);
                if(mfd2osb == 7){
                    if(currentaawpn == "aim120"){
                        currentaawpn = "aim9";
                    } else{
                        currentaawpn = "aim120";
                    }
                    mfd2osb = 21;
                }
                if(mfd2osb == 4){
                    mfd2mode = "smsinv";
                    mfd2osb = 21;
                }
                if(mfd2osb == 14){
                    mfd2mode = "home";
                    mfd2osb = 21;
                }
                if(mfd2osb == 13){
                    mfd2mode = "hsd";
                    mfd2osb = 21;
                }
                if(currentaawpn == "aim120"){
                    aawpnselmfd2.text = "4A - 120C";
                } else{
                    aawpnselmfd2.text = "2A - 9LM";
                }
            }
            if(mfd2mode == "smsag"){
                foreach(GameObject mfd2screen in mfd2screens){
                    mfd2screen.SetActive(false);
                }
                mfd2screens[4].SetActive(true);
                if(currentagmode == "ccip"){
                        mfd2agccipfuzetext.text = mfd2agccipfuze;
                        mfd2agccipwpncnttext.text = mfd2agccipwpncnt;
                        mfd2agccipwpnstatustext.text = mfd2agccipwpnstatus;
                        mfd2agccipsysstatustext.text = mfd2agccipsysstatus;
                        mfd2agccipprofiletext.text = mfd2agccipprofile;
                        mfd2agccipreleasemodetext.text = mfd2agccipreleasemode;
                        mfd2agccipreleaseintervaldisttext.text = mfd2agccipreleaseintervaldist;
                        mfd2agccipreleasepulserequestedtext.text = mfd2agccipreleasepulserequested;
                        if(mfd2osb == 8){
                            if(mfd2agccipreleasemode == "SGL"){
                                mfd2agccipreleasemode = "PAIR";
                            } else{
                                mfd2agccipreleasemode = "SGL";
                            }
                            mfd2osb = 21;
                        }
                        if(mfd2osb == 18){
                            if(mfd2agccipfuze == "NSTL"){
                                mfd2agccipfuze = "NOSE";
                            } else if(mfd2agccipfuze == "NOSE"){
                                mfd2agccipfuze = "TAIL";
                            } else if(mfd2agccipfuze == "TAIL"){
                                mfd2agccipfuze = "NSTL";
                            }
                            mfd2osb = 21;
                        }
                        if(mfd2osb == 9){
                            lastmfd2mode = "smsag";
                            mfddgtentrytopic = "IMPACT SPACING";
                            mfddgtentryunit = "FT";
                            mfd2osb = 21;
                            mfd2mode = "dgtentry";
                            mfd1dgtentryvaltext.text = "";
                            mfd2dgtentryvaltext.text = "";
                        }
                        if(mfd2osb == 10){
                            lastmfd2mode = "smsag";
                            mfddgtentrytopic = "RELEASE PULSES";
                            mfddgtentryunit = " ";
                            mfd2osb = 21;
                            mfd2mode = "dgtentry";
                            mfd1dgtentryvaltext.text = "0";
                            mfd2dgtentryvaltext.text = "0";
                            
                        }
                    }
                if(mfd2osb == 4){
                    mfd2mode = "smsinv";
                    mfd2osb = 21;
                }
                if(mfd2osb == 14){
                    mfd2mode = "home";
                    mfd2osb = 21;
                }
                if(mfd2osb == 13){
                    mfd2mode = "hsd";
                    mfd2osb = 21;
                }
            }
            if(mfd2mode == "hsd"){
                foreach(GameObject mfd2screen in mfd2screens){
                    mfd2screen.SetActive(false);
                }
                mfd2screens[3].SetActive(true);
                for( int i = 0; i < waypointz.Count; i++ ){
                    Vector3 mfd2waypointcrdnate = new Vector3((hsdscale2) * (-waypointz[i] + transform.position.z), (hsdscale2) * (waypointx[i] - transform.position.x), -0.6859215f);
                    if(i == currentstpt - 1){
                        currentwaypointmfd2marker[i].SetActive(true);
                        waypointmfd2marker[i].SetActive(false);
                        currentwaypointmfd2marker[i].transform.localPosition = mfd2waypointcrdnate;
                        currentwaypointmfd2marker[i].transform.localRotation = transform.rotation;
                    } else{
                        waypointmfd2marker[i].SetActive(true);
                        currentwaypointmfd2marker[i].SetActive(false);
                        waypointmfd2marker[i].transform.localPosition = mfd2waypointcrdnate;
                    }
                }
                for( int i = 0; i < waypointmfd2marker.Length; i++ ){
                    if(i > waypointx.Count - 1){
                        waypointmfd2marker[i].SetActive(false);
                        currentwaypointmfd2marker[i].SetActive(false);
                    }
                }
                //mfd1hsdcenter1.localPosition = new Vector3 (-transform.position.x * hsdscale, -transform.position.z * hsdscale, -0.01f);
                //mfd1hsdcenter2.localPosition = new Vector3 (-transform.position.x * hsdscale, -transform.position.z * hsdscale, -0.01f);
                mfd2hsdcenter1.localRotation = Quaternion.Euler(0f, 0f, -(Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg) - angle123);
                mfd2hsdcenter2.localRotation = Quaternion.Euler(0f, 0f, -(Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg) - angle123);
                if(mfd2osb == 13){
                    mfd2mode = "home";
                    mfd2osb = 21;
                }
                if(mfd2osb == 20){
                    hsdscale2 += 0.000001f;
                    mfd2osb = 21;
                }
                if(mfd2osb == 19){
                    hsdscale2 -= 0.000001f;
                    mfd2osb = 21;
                }
                if(mfd2osb == 14){
                    mfd2mode = "smsinv";
                    mfd2osb = 21;
                }
            }

            if(mfd2mode == "dgtentry"){
                foreach(GameObject mfd2screen in mfd2screens){
                    mfd2screen.SetActive(false);
                }
                mfd2screens[5].SetActive(true);
                mfd2dgtentryunittext.text = mfddgtentryunit;
                mfd2dgtentrytopictext.text = mfddgtentrytopic;
                if(mfd2osb == 3){
                    mfd2mode = lastmfd1mode;
                    mfd2dgtentryvaltext.text = "";
                    mfd2osb = 21;
                }
                if(mfd2osb == 2){
                    mfd2mode = lastmfd2mode;
                    mfd2osb = 21;
                    mfddgtentered = true;
                    mfddgtentryid = mfddgtentrytopic;
                    mfddgtentryvalstored = mfd2dgtentryvaltext.text;
                    mfd2dgtentryvaltext.text = "";
                }
                if(mfd2osb != 21){
                    if(mfd2osb == 20){
                        mfddgtentryenteredval = 1;
                    }
                    if(mfd2osb == 19){
                        mfddgtentryenteredval = 2;
                    }
                    if(mfd2osb == 18){
                        mfddgtentryenteredval = 3;
                    }
                    if(mfd2osb == 17){
                        mfddgtentryenteredval = 4;
                    }
                    if(mfd2osb == 16){
                        mfddgtentryenteredval = 5;
                    }
                    if(mfd2osb == 6){
                        mfddgtentryenteredval = 6;
                    }
                    if(mfd2osb == 7){
                        mfddgtentryenteredval = 7;
                    }
                    if(mfd2osb == 8){
                        mfddgtentryenteredval = 8;
                    }
                    if(mfd2osb == 9){
                        mfddgtentryenteredval = 9;
                    }
                    if(mfd2osb == 10){
                        mfddgtentryenteredval = 0;
                    }
                    mfd2osb = 21;
                    mfd2dgtentryvaltext.text += mfddgtentryenteredval.ToString();
                    mfd1dgtentryvaltext.text += mfddgtentryenteredval.ToString();
                }
                mfddgtentryenteredval = 11;
            }

            if(mfd2osb == 15){
                string mfd2mode123 = mfd2mode;
                mfd2mode = mfd1mode;
                mfd1mode = mfd2mode123;
                mfd2osb = 21;
            }
        } else{
            foreach(GameObject mfd2screen in mfd2screens){
                mfd2screen.SetActive(false);
            }
        }
    }

    void CheckDed(){
        if (mainpoweron == true && ufcpoweron == true && rpm60orabove == true){
            if (currentdedpage == "cni"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[0].SetActive(true);
            } else if (currentdedpage == "com1"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[1].SetActive(true);
                if(icpkpadval != 11){
                    if (uhffreqvaldedset.textInfo.characterCount == 6){
                        uhffreqvaldedset.text = "" + icpkpadval.ToString();
                    } else if(uhffreqvaldedset.textInfo.characterCount == 3){
                        uhffreqvaldedset.text += "." + icpkpadval.ToString();
                    } else{
                        uhffreqvaldedset.text += icpkpadval.ToString();
                    }
                } else if(icpkpadval == 11 && icpentrpressed == true){
                    currentuhf = float.Parse(uhffreqvaldedset.text);
                    currentdedpage = "cni";
                }

            } else if (currentdedpage == "com2"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[2].SetActive(true);
                if(icpkpadval != 11){
                    if (vhffreqvaldedset.textInfo.characterCount == 6){
                        vhffreqvaldedset.text = "" + icpkpadval.ToString();
                    } else if(vhffreqvaldedset.textInfo.characterCount == 3){
                        vhffreqvaldedset.text += "." + icpkpadval.ToString();
                    } else{
                        vhffreqvaldedset.text += icpkpadval.ToString();
                    }
                } else if(icpkpadval == 11 && icpentrpressed == true){
                    currentvhf = float.Parse(vhffreqvaldedset.text);
                    currentdedpage = "cni";
                }
            } else if (currentdedpage == "tils"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[3].SetActive(true);
            } else if (currentdedpage == "alow"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[4].SetActive(true);
            } else if (currentdedpage == "stpt"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[5].SetActive(true);
                if(icpseq == true){
                    if(currentstptmode == "AUTO"){
                        currentstptmode = "MAN";
                    } else{
                        currentstptmode = "AUTO";
                    }
                }
                if(currentstptmode == "MAN"){
                    if(icprtnsequp == true || Input.GetKeyUp(KeyCode.P)){
                        if(currentstpt == waypointx.Count && currentstpt == waypointz.Count){
                            currentstpt = 1;
                        } else{
                            currentstpt += 1;
                        }
                    }
                    if(icprtnseqdown == true || Input.GetKeyUp(KeyCode.O)){
                        if(currentstpt == 1){
                            currentstpt = waypointx.Count;
                        } else{
                            currentstpt -= 1;
                        }
                    }
                }

            } else if (currentdedpage == "time"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[6].SetActive(true);
                if(icprtnsequp == true){
                    if(timepageselection == "date"){
                        timepageselection = "tos";
                    } else if(timepageselection == "tos"){
                        timepageselection = "hack";
                    } else if(timepageselection == "hack"){
                        timepageselection = "date";
                    }
                } else if(icprtnseqdown == true){
                    if(timepageselection == "hack"){
                        timepageselection = "tos";
                    } else if(timepageselection == "tos"){
                        timepageselection = "date";
                    } else if(timepageselection == "date"){
                        timepageselection = "hack";
                    }
                }
                if(timepageselection != "date"){
                    tpagedatestar.gameObject.SetActive(false);
                }
                if(timepageselection != "hack"){
                    tpagehackstar.gameObject.SetActive(false);
                }
                if(timepageselection != "tos"){
                    tpagetosstar.gameObject.SetActive(false);
                }
                if(timepageselection == "hack"){
                    tpagehackstar.gameObject.SetActive(true);
                    if(hacktimerrunning == false && icpkpadval != 11){
                        if(tpghtimecrsr == 1){
                            tpghtimecrsr = 2;
                            hackhour = icpkpadval;
                            hackmin = 0;
                            hacksec = 0;
                        } else if(tpghtimecrsr == 2){
                            hackhour = (hackhour * 10) + icpkpadval;
                            hackmin = 0;
                            hacksec = 0;
                            tpghtimecrsr = 4;
                        } else if(tpghtimecrsr == 4){
                            hackmin = icpkpadval;
                            hacksec = 0;
                            tpghtimecrsr = 5;
                        } else if(tpghtimecrsr == 5){
                            hackmin = (hackmin * 10) + icpkpadval;
                            hacksec = 0;
                            tpghtimecrsr = 7;
                        } else if(tpghtimecrsr == 7){
                            hacksec = icpkpadval;
                            tpghtimecrsr = 8;
                        } else if(tpghtimecrsr == 8){
                            hacksec = (hacksec * 10) + icpkpadval;
                            tpghtimecrsr = 1;
                        }
                    }
                    if(icpleftup == true){
                        if(hacktimerrunning == true){
                            hacktimerrunning = false;
                        } else{
                            hacktimerrunning = true;
                        }
                    } else if(icpleftdown == true){
                        hackhour = 0;
                        hackmin = 0;
                        hacksec = 0;
                    }
                } else if(timepageselection == "tos"){
                    tpagetosstar.gameObject.SetActive(true);
                } else if(timepageselection == "date"){
                    tpagedatestar.gameObject.SetActive(true);
                }
            } else if (currentdedpage == "list"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[7].SetActive(true);
                if (icpkpadval == 2){
                    currentdedpage = "bingo";
                } else if (icpkpadval == 4){
                    currentdedpage = "navstat";
                } else if (icpkpadval == 5){
                    currentdedpage = "man";
                } else if (icpkpadval == 6){
                    currentdedpage = "ins";
                } else if (icpkpadval == 7){
                    currentdedpage = "cmds1";
                } else if (icpkpadval == 8){
                    currentdedpage = "mode";
                } else if (icpkpadval == 0){
                    currentdedpage = "misc";
                } else if(Input.GetKeyUp(KeyCode.E)){
                    currentdedpage = "dlink1";
                }
            } else if (currentdedpage == "bingo"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[8].SetActive(true);
                if(icpkpadval != 11){
                    if (bingoset.textInfo.characterCount == 5){
                        bingoset.text = "" + icpkpadval.ToString();
                    } else{
                        bingoset.text += icpkpadval.ToString();
                    }
                } else if(icpkpadval == 11 && icpentrpressed == true && float.Parse(bingoset.text) < currentfuelleft){
                    currentbingo = float.Parse(bingoset.text);
                    currentdedpage = "cni";
                    warnsilence = false;
                }
            } else if (currentdedpage == "navstat"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[9].SetActive(true);
                if(icpseq == true){
                    currentdedpage = "navcomms";
                }
            } else if (currentdedpage == "navcomms"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[10].SetActive(true);
                if(icpseq == true){
                    currentdedpage = "navstat";
                } else if(icpkpadval != 11){
                    if(egifiltermode1 == "AUTO"){
                        egifiltermode1 = "INS";
                    } else{
                        egifiltermode1 = "AUTO";
                    }
                }
            } else if (currentdedpage == "man"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[11].SetActive(true);
            } else if (currentdedpage == "ins"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[12].SetActive(true);
            } else if (currentdedpage == "dlink1"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[13].SetActive(true);
            } else if (currentdedpage == "dlink2"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[14].SetActive(true);
            } else if (currentdedpage == "dlink3"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[15].SetActive(true);
            } else if (currentdedpage == "cmds1"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[16].SetActive(true);
            } else if (currentdedpage == "cmds2"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[17].SetActive(true);
            } else if (currentdedpage == "cmds3"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[18].SetActive(true);
            } else if (currentdedpage == "misc"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[19].SetActive(true);
                if (icpkpadval == 2){
                    currentdedpage = "magv";
                } else if (icpkpadval == 5){
                    currentdedpage = "lasr";
                }
            } else if (currentdedpage == "mode"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[20].SetActive(true);
            } else if (currentdedpage == "magv"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[21].SetActive(true);
                if(icpseq == true){
                    if (magvmode1 == "AUTO"){
                        magvmode1 = "MAN";
                    } else if (magvmode1 == "MAN"){
                        magvmode1 = "AUTO";
                    }
                }
                if (magvmode1 == "MAN"){
                    if (icpkpadval != 11){
                        if(magvval.textInfo.characterCount == 6){
                            magvval1 = "" + icpkpadval.ToString();
                        } else if(magvval.textInfo.characterCount == 3){
                            magvval1 += "." + icpkpadval.ToString();
                        } else{
                            magvval1 += icpkpadval.ToString();
                        }
                    } else if(icpkpadval == 11 && icpentrpressed == true){
                        currentdedpage = "cni";
                    }
                    if(icpleftup == true){
                        magvdir1 = "E";
                    } else if(icpleftdown == true){
                        magvdir1 = "W";
                    }
                }
            } else if (currentdedpage == "lasr"){
                foreach(GameObject dedpage in dedpages){
                    dedpage.SetActive(false);
                }
                dedpages[22].SetActive(true);
            }
        } else{
            foreach(GameObject dedpage in dedpages){
                dedpage.SetActive(false);
            }
        }
        dedcnistptval.text = currentstpt.ToString();
        dedcniuhfval.text = currentuhf.ToString("F2");
        dedcnivhfval.text = currentvhf.ToString("F2");
        uhffreqvaldedset1.text = currentuhf.ToString("F2");
        vhffreqvaldedset1.text = currentvhf.ToString("F2");
        bingocurrentfuelleft.text = currentfuelleft.ToString("F0");
        magvmode.text = magvmode1;
        magvval.text = magvval1;
        magvdir.text = magvdir1;
        egifiltermode.text = egifiltermode1;
        stptpagemode.text = currentstptmode;
        stptpagestptval.text = currentstpt.ToString();
    }

    void CheckFuel(){
        if (mainpoweron == true && jetfueloff == false && righthpt == true && lefthpt == true && fueltranswingfirst == false){
            currentfuelleft = CenterlineTank + A1 + F1F2 + AFTRSVR + FWDRSVR + RightInterior + LeftInterior + RightExterior + LeftExterior;
        } else if( mainpoweron == true && jetfueloff == false && righthpt == false && lefthpt == false && fueltranswingfirst == false ){
            currentfuelleft = CenterlineTank + A1 + F1F2 + AFTRSVR + FWDRSVR + RightInterior + LeftInterior;
        } else if( mainpoweron == true && jetfueloff == false && righthpt == false && lefthpt == false && fueltranswingfirst == true ){
            currentfuelleft = 0f;
        } else if( mainpoweron == true && jetfueloff == false && righthpt == true && lefthpt == true && fueltranswingfirst == true ){
            currentfuelleft = RightExterior + LeftExterior;
        }
        currentfuelpercentage = (currentfuelleft/totalfuel) * 100f;
        if (fueltranswingfirst == false && mainpoweron == true && jetfueloff == false && righthpt == true && lefthpt == true){
            if (CenterlineTank > 0f){
                CenterlineTank -= (currentenginepower/maxenginepower) * 18f * Time.deltaTime;
            } else if(A1 > 0f){
                A1 -= (currentenginepower/maxenginepower) * 18f * Time.deltaTime;
            } else if(F1F2 > 0f){
                F1F2 -= (currentenginepower/maxenginepower) * 18f * Time.deltaTime;
            } else if(AFTRSVR > 0f && FWDRSVR != 0f){
                AFTRSVR -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
                FWDRSVR -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
            } else if (RightInterior > 0f && LeftInterior != 0f){
                RightInterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
                LeftInterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
            } else if (RightExterior > 0f && LeftExterior != 0f && righthpt == true && lefthpt == true){
                RightExterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
                LeftExterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
            }

        } else if (fueltranswingfirst == true  && mainpoweron == true && jetfueloff == false && rpm60orabove == true){
            if (RightExterior > 0f && LeftExterior != 0f && righthpt == true && lefthpt == true){
                RightExterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
                LeftExterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
            } else if (RightInterior > 0f && LeftInterior != 0f){
                RightInterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
                LeftInterior -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
            } else if(AFTRSVR > 0f && FWDRSVR != 0f){
                AFTRSVR -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
                FWDRSVR -= ((currentenginepower/maxenginepower) * 18f * Time.deltaTime) / 2f;
            } else if(F1F2 > 0f){
                F1F2 -= (currentenginepower/maxenginepower) * 18f * Time.deltaTime;
            } else if(A1 > 0f){
                A1 -= (currentenginepower/maxenginepower) * 18f * Time.deltaTime;
            } else if (CenterlineTank > 0f){
                CenterlineTank -= (currentenginepower/maxenginepower) * 18f * Time.deltaTime;
            }
        }
        if(rpm60orabove == true){
            fuelflowinpph = 1200 + (currentenginepower/maxenginepower) * 62800f;
        } else{
            fuelflowinpph = 0;
        }

        if (mainpoweron == true){
            fuelindicatortotfuelleft.gameObject.SetActive(true);
            cockpitfuelflow.gameObject.SetActive(true);
            if(rpm60orabove == true){
                if (fuelgaugedispstate == 1){
                    fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210 + (A1/100) * 23f/4f , 0f);
                    fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 + (F1F2/100) * 23f/4f , 0f);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(17.333f, -2.273f, -19.508f);
                } else if(fuelgaugedispstate == 2){
                    fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210 + (AFTRSVR/100) * 23f/4f , 0f);
                    fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 + (FWDRSVR/100) * 23f/4f , 0f);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(17.333f, -2.273f, -2.652f);
                } else if(fuelgaugedispstate == 3){
                    fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210 + (RightInterior/100) * 23f/4f , 0f);
                    fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 + (LeftInterior/100) * 23f/4f , 0f);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(17.333f, -2.273f, 46.516f);
                } else if(fuelgaugedispstate == 4){
                    fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210 + (RightExterior/100) * 23f/4f , 0f);
                    fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 + (LeftExterior/100) * 23f/4f , 0f);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(17.333f, -2.273f, 105.714f);
                } else if(fuelgaugedispstate == 5){
                    fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210 + (CenterlineTank/100) * 23f/4f , 0f);
                    fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 + (CenterlineTank/100) * 23f/4f , 0f);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(17.333f, -2.273f, 155.391f);
                } else if(fuelgaugedispstate == 0){
                    fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210 + (20/100) * 23f/4f , 0f);
                    fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 + (20/100) * 23f/4f , 0f);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(17.333f, -2.273f, -87.034f);
                }
                fuelindicatortotfuelleft.text = currentfuelleft.ToString("F0");
            } else{
                fuelgaugepointer1.localRotation = Quaternion.Euler(0f, -210, 0f);
                fuelgaugepointer2.localRotation = Quaternion.Euler(0f, -210 , 0f);
            }
            cockpitfuelflow.text = fuelflowinpph.ToString("F0");
        } else {
            fuelindicatortotfuelleft.gameObject.SetActive(false);
            cockpitfuelflow.gameObject.SetActive(false);
        }
    }

    void CheckInstrumentSelection(){
        RaycastHit rayHit1;
        if (_selection != null){
            var selectionrenderer = _selection.GetComponent<Renderer>();
            selectionrenderer.material.SetColor("_Color", Color.white);
            _selection = null;
        }
        if( Physics.Raycast(viewcams[0].ScreenPointToRay(Input.mousePosition), out rayHit1, Mathf.Infinity, selectableinstrument) == false || rayHit1.collider.tag != "ilstcncrsrot")
        {
            if (Input.GetMouseButton(0) == false)
            {
                hsicrsrotmouseup = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.Keypad1)){
            if (currentdedpage == "cni"){
                currentdedpage = "tils";
            } else{
                icpkpadval = 1;
            }
        }if(Input.GetKeyUp(KeyCode.Keypad2)){
            if (currentdedpage == "cni"){
                currentdedpage = "alow";
            } else {
                icpkpadval = 2;
            }
        }if(Input.GetKeyUp(KeyCode.Keypad3)){
            icpkpadval = 3;
        }if(Input.GetKeyUp(KeyCode.Keypad4)){
            if (currentdedpage == "cni"){
                currentdedpage = "stpt";
            } else {
                icpkpadval = 4;
            }
        }if(Input.GetKeyUp(KeyCode.Keypad5)){
            icpkpadval = 5;
        }if(Input.GetKeyUp(KeyCode.Keypad6)){
            if (currentdedpage == "cni"){
                currentdedpage = "time";
            } else {
                icpkpadval = 6;
            }
        }if(Input.GetKeyUp(KeyCode.Keypad7)){
            icpkpadval = 7;
        }if(Input.GetKeyUp(KeyCode.Keypad8)){
            icpkpadval = 8;
        }if(Input.GetKeyUp(KeyCode.Keypad9)){
            icpkpadval = 9;
        }if(Input.GetKeyUp(KeyCode.Keypad0)){
            icpkpadval = 0;
        }if(Input.GetKeyUp(KeyCode.KeypadEnter)){
            icpentrpressed = true;
        }if(Input.GetKeyUp(KeyCode.KeypadPeriod)){
            currentdedpage = "cni";
            icprtn = true;
        }if(Input.GetKey(KeyCode.KeypadPlus)){
            icphudbrtrotval = Mathf.Clamp(icphudbrtrotval + 60, 0f, 360f);
            icphudcontrotval = Mathf.Clamp(icphudcontrotval + 60, 0f, 360f);
            icphudsymrotval = Mathf.Clamp(icphudsymrotval + 60, 0f, 360f);
        }if(Input.GetKey(KeyCode.KeypadMinus)){
            icphudbrtrotval = Mathf.Clamp(icphudbrtrotval - 60, 0f, 360f);
            icphudcontrotval = Mathf.Clamp(icphudcontrotval - 60, 0f, 360f);
            icphudsymrotval = Mathf.Clamp(icphudsymrotval - 60, 0f, 360f);
        }
        if( Physics.Raycast(viewcams[0].ScreenPointToRay(Input.mousePosition), out rayHit1, Mathf.Infinity, selectableinstrument)){
            if (Input.GetMouseButtonUp(0) == true){
                if(rayHit1.collider.tag == "huddedswitch"){
                    if(huddedswitchstate == 0){
                        huddedswitchstate = 1;
                        huddedswitchtrans.localRotation = Quaternion.Euler(16.119f, 0f, 0f);
                    }else if(huddedswitchstate == 1){
                        huddedswitchstate = 2;
                        huddedswitchtrans.localRotation = Quaternion.Euler(35.041f, 0f, 0f);
                    }
                }
                if(rayHit1.collider.tag == "aammswitch"){
                    if(mastermode != "aa"){
                        mastermode = "aa";
                        mfd1mode = "smsaa";
                        mfd2mode = "smsinv";
                    } else{
                        mastermode = "nav";
                        mfd1mode = "home";
                        mfd2mode = "home";
                    }
                }
                if(rayHit1.collider.tag == "agmmswitch"){
                    if(mastermode != "ag"){
                        mastermode = "ag";
                        mfd1mode = "smsag";
                        mfd2mode = "smsinv";
                    } else{
                        mastermode = "nav";
                        mfd1mode = "home";
                        mfd2mode = "home";
                    }
                }
                if(rayHit1.collider.tag == "mfd1osb1"){
                    mfd1osb = 1;
                }
                if(rayHit1.collider.tag == "mfd1osb2"){
                    mfd1osb = 2;
                }
                if(rayHit1.collider.tag == "mfd1osb3"){
                    mfd1osb = 3;
                }
                if(rayHit1.collider.tag == "mfd1osb4"){
                    mfd1osb = 4;
                }
                if(rayHit1.collider.tag == "mfd1osb5"){
                    mfd1osb = 5;
                }
                if(rayHit1.collider.tag == "mfd1osb6"){
                    mfd1osb = 6;
                }
                if(rayHit1.collider.tag == "mfd1osb7"){
                    mfd1osb = 7;
                }
                if(rayHit1.collider.tag == "mfd1osb8"){
                    mfd1osb = 8;
                }
                if(rayHit1.collider.tag == "mfd1osb9"){
                    mfd1osb = 9;
                }
                if(rayHit1.collider.tag == "mfd1osb10"){
                    mfd1osb = 10;
                }
                if(rayHit1.collider.tag == "mfd1osb11"){
                    mfd1osb = 11;
                }
                if(rayHit1.collider.tag == "mfd1osb12"){
                    mfd1osb = 12;
                }
                if(rayHit1.collider.tag == "mfd1osb13"){
                    mfd1osb = 13;
                }
                if(rayHit1.collider.tag == "mfd1osb14"){
                    mfd1osb = 14;
                }
                if(rayHit1.collider.tag == "mfd1osb15"){
                    mfd1osb = 15;
                }
                if(rayHit1.collider.tag == "mfd1osb16"){
                    mfd1osb = 16;
                }
                if(rayHit1.collider.tag == "mfd1osb17"){
                    mfd1osb = 17;
                }
                if(rayHit1.collider.tag == "mfd1osb18"){
                    mfd1osb = 18;
                }
                if(rayHit1.collider.tag == "mfd1osb19"){
                    mfd1osb = 19;
                }
                if(rayHit1.collider.tag == "mfd1osb20"){
                    mfd1osb = 20;
                }
                if(rayHit1.collider.tag == "mfd2osb1"){
                    mfd2osb = 1;
                }
                if(rayHit1.collider.tag == "mfd2osb2"){
                    mfd2osb = 2;
                }
                if(rayHit1.collider.tag == "mfd2osb3"){
                    mfd2osb = 3;
                }
                if(rayHit1.collider.tag == "mfd2osb4"){
                    mfd2osb = 4;
                }
                if(rayHit1.collider.tag == "mfd2osb5"){
                    mfd2osb = 5;
                }
                if(rayHit1.collider.tag == "mfd2osb6"){
                    mfd2osb = 6;
                }
                if(rayHit1.collider.tag == "mfd2osb7"){
                    mfd2osb = 7;
                }
                if(rayHit1.collider.tag == "mfd2osb8"){
                    mfd2osb = 8;
                }
                if(rayHit1.collider.tag == "mfd2osb9"){
                    mfd2osb = 9;
                }
                if(rayHit1.collider.tag == "mfd2osb10"){
                    mfd2osb = 10;
                }
                if(rayHit1.collider.tag == "mfd2osb11"){
                    mfd2osb = 11;
                }
                if(rayHit1.collider.tag == "mfd2osb12"){
                    mfd2osb = 12;
                }
                if(rayHit1.collider.tag == "mfd2osb13"){
                    mfd2osb = 13;
                }
                if(rayHit1.collider.tag == "mfd2osb14"){
                    mfd2osb = 14;
                }
                if(rayHit1.collider.tag == "mfd2osb15"){
                    mfd2osb = 15;
                }
                if(rayHit1.collider.tag == "mfd2osb16"){
                    mfd2osb = 16;
                }
                if(rayHit1.collider.tag == "mfd2osb17"){
                    mfd2osb = 17;
                }
                if(rayHit1.collider.tag == "mfd2osb18"){
                    mfd2osb = 18;
                }
                if(rayHit1.collider.tag == "mfd2osb19"){
                    mfd2osb = 19;
                }
                if(rayHit1.collider.tag == "mfd2osb20"){
                    mfd2osb = 20;
                }
                if(rayHit1.collider.tag == "icprtnsequp"){
                    icprtnsequp = true;
                }
                if(rayHit1.collider.tag == "icprtnseqdown"){
                    icprtnseqdown = true;
                }
                if(rayHit1.collider.tag == "avionicspanufc"){
                    if(ufcpoweron == true){
                        ufcpoweron = false;
                    } else{
                        ufcpoweron = true;
                    }
                }
                if(rayHit1.collider.tag == "avionicspanmfd"){
                    if(mfdpoweron == true){
                        mfdpoweron = false;
                    } else{
                        mfdpoweron = true;
                    }
                }
                if(rayHit1.collider.tag == "avionicspanststa"){
                    if(ststapoweron == true){
                        ststapoweron = false;
                    } else{
                        ststapoweron = true;
                    }
                }
                if(rayHit1.collider.tag == "avionicspanmmc"){
                    if(mmcpoweron == true){
                        mmcpoweron = false;
                    } else{
                        mmcpoweron = true;
                    }
                }
                if(rayHit1.collider.tag == "avionicspangps"){
                    if(gpspoweron == true){
                        gpspoweron = false;
                    } else{
                        gpspoweron = true;
                    }
                }
                if(rayHit1.collider.tag == "avionicspandl"){
                    if(dlpoweron == true){
                        dlpoweron = false;
                    } else{
                        dlpoweron = true;
                    }
                }
                if(rayHit1.collider.tag == "icpleftup"){
                    icpleftup = true;
                }
                if(rayHit1.collider.tag == "icpleftdown"){
                    icpleftdown = true;
                }
                if(rayHit1.collider.tag == "icplist"){
                    currentdedpage = "list";
                }
                if(rayHit1.collider.tag == "icpentr"){
                    icpentrpressed = true;
                }
                if(rayHit1.collider.tag == "icptils1"){
                    if (currentdedpage == "cni"){
                        currentdedpage = "tils";
                    } else{
                        icpkpadval = 1;
                    }
                }
                if(rayHit1.collider.tag == "icpalow2"){
                    if (currentdedpage == "cni"){
                        currentdedpage = "alow";
                    } else {
                        icpkpadval = 2;
                    }
                }
                if(rayHit1.collider.tag == "icp3"){
                    icpkpadval = 3;
                }
                if(rayHit1.collider.tag == "icpstpt4"){
                    if (currentdedpage == "cni"){
                        currentdedpage = "stpt";
                    } else {
                        icpkpadval = 4;
                    }
                }
                if(rayHit1.collider.tag == "icpcrus5"){
                    icpkpadval = 5;
                }
                if(rayHit1.collider.tag == "icptime6"){
                    if (currentdedpage == "cni"){
                        currentdedpage = "time";
                    } else {
                        icpkpadval = 6;
                    }
                }
                if(rayHit1.collider.tag == "icpmark7"){
                    icpkpadval = 7;
                }
                if(rayHit1.collider.tag == "icpfix8"){
                    icpkpadval = 8;
                }
                if(rayHit1.collider.tag == "icpacal9"){
                    icpkpadval = 9;
                }
                if(rayHit1.collider.tag == "icpmsel0"){
                    icpkpadval = 0;
                }
                if(rayHit1.collider.tag == "com1icp"){
                    currentdedpage = "com1";
                }
                if(rayHit1.collider.tag == "com2icp"){
                    currentdedpage = "com2";
                }
                if(rayHit1.collider.tag == "icprtnseq"){
                    currentdedpage = "cni";
                    icprtn = true;
                }
                if(rayHit1.collider.tag == "icphudbrt"){
                    icphudbrtrotval = Mathf.Clamp(icphudbrtrotval + 60, 0f, 360f);
                }
                if(rayHit1.collider.tag == "icphudcont"){
                    icphudcontrotval = Mathf.Clamp(icphudcontrotval + 60, 0f, 360f);
                }
                if(rayHit1.collider.tag == "icphudsym"){
                    icphudsymrotval = Mathf.Clamp(icphudsymrotval + 60, 0f, 360f);
                }

                if ( rayHit1.collider.tag == "acollmoderot"){
                    acollrotstate = Mathf.Clamp(acollrotstate + 1, 0, 6);
                }
                if (rayHit1.collider.tag == "extflightrot"){
                    flightrotstate = Mathf.Clamp(flightrotstate + 50f, 0f, 260f);

                }

                if (rayHit1.collider.tag == "wingtaillightswitch"){
                    if ( wtbrtswitchstate == 2 ){
                        wtbrtswitchstate = 0;
                    } else if ( wtbrtswitchstate == 0 ){
                        wtbrtswitchstate = 1;
                    }
                }
                if (rayHit1.collider.tag == "flagebrtswitch"){
                    if ( flagebrtswitchstate == 2 ){
                        flagebrtswitchstate = 0;
                    } else if ( flagebrtswitchstate == 0 ){
                        flagebrtswitchstate = 1;
                    }
                }
                if (rayHit1.collider.tag == "extlightflashswitch"){
                    if ( poslightfsswitchstate == 1 ){
                        poslightfsswitchstate = 0;
                    } else if ( poslightfsswitchstate == 0 ){
                        poslightfsswitchstate = 1;
                    }
                }
                if (rayHit1.collider.tag == "extmlightrot"){
                    if ( masterlightrotstate == 0 ){
                        masterlightrotstate = 1;
                    } else {
                        masterlightrotstate = 0;
                    }
                }

                if (rayHit1.collider.tag == "taxilightswitch"){
                    if ( gearlightswitchstate == 2 ){
                        gearlightswitchstate = 0;
                    } else if ( gearlightswitchstate == 0 ){
                        gearlightswitchstate = 1;
                    }
                }

                if (rayHit1.collider.tag == "gearlever"){
                    if(gearup == true){
                        gearup = false;
                    } else if (gearup == false && isgrounded == false){
                        gearup = true;
                    }
                } else
                if (rayHit1.collider.tag == "powerswitch"){
                    if (mainpoweron == true){
                        mainpoweron = false;
                        powerswitch.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    } else if (mainpoweron == false){
                        mainpoweron = true;
                        powerswitch.localRotation = Quaternion.Euler(55.32f, 0f, 0f);
                    }
                }
                if (rayHit1.collider.tag == "jetfuelswitch"){
                    if (jetfuelstart1 == true && jetfueloff == false && jetfuelstart2 == false && rpmangle >= -70f){
                        jetfuelstart1 = false;
                        jetfuelstart2 = false;
                        jetfueloff = false;
                        jfs.localRotation = Quaternion.Euler(18.384f, 0f, 0f);
                    } else if (jetfuelstart2 == true){
                        jetfuelstart1 = true;
                        jetfueloff = false;
                        jetfuelstart2 = false;
                        jfs.localRotation = Quaternion.Euler(43.688f, 0f, 0f);
                    } else if (jetfueloff == true){
                        jetfuelstart1 = true;
                        jetfueloff = false;
                        jetfuelstart2 = false;
                        jfs.localRotation = Quaternion.Euler(43.688f, 0f, 0f);
                    } else if(jetfuelstart1 == true && jetfueloff == false && jetfuelstart2 == false){
                        jetfuelstart1 = false;
                        jetfuelstart2 = false;
                        jetfueloff = true;
                        jfs.localRotation = Quaternion.Euler(18.384f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "masterarmswitch"){
                    if (masterarmstate == 0){
                        masterarmstate = 1;
                        masterarmswitch.localRotation = Quaternion.Euler(0f, 35.127f, 0f);
                        hudarm.text = "ARM".ToString();
                    } else if(masterarmstate == 2){
                        masterarmstate = 1;
                        masterarmswitch.localRotation = Quaternion.Euler(0f, 35.127f, 0f);
                        hudarm.text = "ARM".ToString();
                    } else{
                        masterarmstate = 0;
                        masterarmswitch.localRotation = Quaternion.Euler(0f, 10.231f, 0f);
                        hudarm.text = " ".ToString();
                    }
                }


                if (rayHit1.collider.tag == "parkingbreakswitch"){
                    if (parkingbreak == true){
                        parkingbreak = false;
                        parkingbreakswitch.localRotation = Quaternion.Euler(0f, -1.641f, 0f);
                    } else{
                        parkingbreak = true;
                        parkingbreakswitch.localRotation = Quaternion.Euler(0f, 43.322f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "hudaltrdrswitch"){
                    if (hudaltstate == 0){
                        hudaltstate = 1;
                        hudaltswitch.localRotation = Quaternion.Euler(20.839f, 0f, 0f);
                    } else if(hudaltstate == 1 && altrdrstate == true){
                        hudaltstate = 2;
                        hudaltswitch.localRotation = Quaternion.Euler(39.416f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "rdraltswitch"){
                    if (altrdrstate == true){
                        altrdrstate = false;
                        altrdrpowerswitch.localRotation = Quaternion.Euler(-4.318f, 0f, 0f);
                    } else {
                        altrdrstate = true;
                        altrdrpowerswitch.localRotation = Quaternion.Euler(43.733f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "fcrpowerswitch"){
                    if (firecontrolradarstate == 0){
                        firecontrolradarstate = 1;
                        fcrpowerswitch.localRotation = Quaternion.Euler(46.075f, 0f, 0f);
                    } else {
                        firecontrolradarstate = 0;
                        fcrpowerswitch.localRotation = Quaternion.Euler(-7.421f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "hudattfpmswitch"){
                    if (hudfpmattswitchpos == 0){
                        hudfpmattswitchpos = 1;
                        hudfpmattswitch.localRotation = Quaternion.Euler(21.093f, 0f, 0f);
                        altladder.gameObject.SetActive(false);
                    } else {
                        hudfpmattswitchpos = 0;
                        hudfpmattswitch.localRotation = Quaternion.Euler(-3.986f, 0f, 0f);
                        altladder.gameObject.SetActive(false);
                    }
                }
                if (rayHit1.collider.tag == "consolefloodset" && mainpoweron == true){
                    consolefloodlightsrotaryxrotation = Mathf.Clamp(consolefloodlightsrotaryxrotation + 50f, 0f, 180f);
                    foreach (Light conlight in consolefloodlights){
                        conlight.intensity = consolefloodlightsrotaryxrotation/20f;
                    }
                    consolefloodlightsrotary.localRotation = Quaternion.Euler(-consolefloodlightsrotaryxrotation, 110.441f, 89.005f);
                }

                if (rayHit1.collider.tag == "instprilights" && mainpoweron == true){
                    priinstlightsrotaryxrotation = Mathf.Clamp(priinstlightsrotaryxrotation + 50f, 0f, 180f);
                    foreach (Light priinstlight in priinstlights){
                        priinstlight.intensity = priinstlightsrotaryxrotation * 5.55555556f;
                    }
                    priinstlightsrotary.localRotation = Quaternion.Euler(priinstlightsrotaryxrotation, -68.938f, -88.93201f);
                }

                if (rayHit1.collider.tag == "hudspddispswitch"){
                    hudairspeedswitchpos = Mathf.Clamp(hudairspeedswitchpos + 1, 0, 2);
                    if(hudairspeedswitchpos == 0){
                        hudairspeedswitch.localRotation = Quaternion.Euler(-0.397f, 0f, 0f);
                    }
                    if(hudairspeedswitchpos == 1){
                        hudairspeedswitch.localRotation = Quaternion.Euler(23.758f, 0f, 0f);
                    }
                    if(hudairspeedswitchpos == 2){
                        hudairspeedswitch.localRotation = Quaternion.Euler(50.047f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "lefthptswitch"){
                    if (lefthpt == false){
                        lefthpt = true;
                        lefthptpowerswitch.localRotation = Quaternion.Euler(39.076f, 0f, 0f);
                    } else {
                        lefthpt = false;
                        lefthptpowerswitch.localRotation = Quaternion.Euler(-4.812f, 0f, 0f);
                    }
                }
                if (rayHit1.collider.tag == "righthptswitch"){
                    if (righthpt == false){
                        righthpt = true;
                        righthptpowerswitch.localRotation = Quaternion.Euler(41.008f, 0f, 0f);
                    } else {
                        righthpt = false;
                        righthptpowerswitch.localRotation = Quaternion.Euler(-3.419f, 0f, 0f);
                    }
                }
                if (rayHit1.collider.tag == "extfueltransswitch"){
                    if (fueltranswingfirst == true){
                        fueltranswingfirst = false;
                        extfueltransswitch.localRotation = Quaternion.Euler(0f, -17.98f, 0f);
                    } else {
                        fueltranswingfirst = true;
                        extfueltransswitch.localRotation = Quaternion.Euler(0f, 35.617f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "fuelgaugemoderotary"){
                    fuelgaugedispstate = Mathf.Clamp(fuelgaugedispstate + 1, 0, 5);
                }

                if (rayHit1.collider.tag == "priconsolelightrot" && mainpoweron == true){
                    priconsolelightrotaryxrot = Mathf.Clamp(priconsolelightrotaryxrot + 50f, 0f, 180f);
                    consoleprilightsrotary.localRotation = Quaternion.Euler(priconsolelightrotaryxrot, -69.55901f, 90f);
                    if (priconsolelightrotaryxrot > 0){
                        foreach (TextMeshPro cockpittmp in cockpittmps){
                            Color32 col1 = new Color(0f, (priconsolelightrotaryxrot*(51/36))/255f, (priconsolelightrotaryxrot*(29/36))/255f);
                            cockpittmp.color = col1;
                        }   
                    } else {
                        foreach (TextMeshPro cockpittmp in cockpittmps){
                            Color32 col2 = new Color(1f, 1f,1f);
                            cockpittmp.color = col2;
                        } 
                    }
                }

                if (rayHit1.collider.tag == "arfuelingrot"){
                    arfuelingrotstate = Mathf.Clamp(arfuelingrotstate + 40, 0f, 270f);
                }
                
            }

            if(Input.GetMouseButton(0) == true){
                if(rayHit1.collider.tag == "ilstcncrsrot"){
                    hsicrsrotmouseup = false;   
                }
            }

            if (Input.GetMouseButtonUp(1) == true){
                if(rayHit1.collider.tag == "huddedswitch"){
                    if(huddedswitchstate == 2){
                        huddedswitchstate = 1;
                        huddedswitchtrans.localRotation = Quaternion.Euler(16.119f, 0f, 0f);
                    }else if(huddedswitchstate == 1){
                        huddedswitchstate = 0;
                        huddedswitchtrans.localRotation = Quaternion.Euler(-6.61f, 0f, 0f);
                    }
                }
                if(rayHit1.collider.tag == "icprtnseq"){
                    icpseq = true;
                }
                if(rayHit1.collider.tag == "icpdriftco"){
                    warnsilence = true;
                }
                if(rayHit1.collider.tag == "icphudcont"){
                    icphudcontrotval = Mathf.Clamp(icphudcontrotval - 60, 0f, 360f);
                }
                if(rayHit1.collider.tag == "icphudbrt"){
                    icphudbrtrotval = Mathf.Clamp(icphudbrtrotval - 60, 0f, 360f);
                }
                if(rayHit1.collider.tag == "icphudsym"){
                    icphudsymrotval = Mathf.Clamp(icphudsymrotval - 60, 0f, 360f);
                }
                if (rayHit1.collider.tag == "arfuelingrot"){
                    arfuelingrotstate = Mathf.Clamp(arfuelingrotstate - 40, 0f, 270f);
                }
                if ( rayHit1.collider.tag == "acollmoderot"){
                    acollrotstate = Mathf.Clamp(acollrotstate - 1, 0, 6);
                }
                if (rayHit1.collider.tag == "extflightrot"){
                    flightrotstate = Mathf.Clamp(flightrotstate - 50f, 0f, 260f);
                }
                if (rayHit1.collider.tag == "wingtaillightswitch"){
                    if ( wtbrtswitchstate == 1 ){
                        wtbrtswitchstate = 0;
                    } else if ( wtbrtswitchstate == 0 ){
                        wtbrtswitchstate = 2;
                    }
                }
                if (rayHit1.collider.tag == "flagebrtswitch"){
                    if ( flagebrtswitchstate == 1 ){
                        flagebrtswitchstate = 0;
                    } else if ( flagebrtswitchstate == 0 ){
                        flagebrtswitchstate = 2;
                    }
                }
                if (rayHit1.collider.tag == "taxilightswitch"){
                    if ( gearlightswitchstate == 1 ){
                        gearlightswitchstate = 0;
                    } else if ( gearlightswitchstate == 0 ){
                        gearlightswitchstate = 2;
                    }
                }

                if (rayHit1.collider.tag == "priconsolelightrot" && mainpoweron == true){
                    priconsolelightrotaryxrot = Mathf.Clamp(priconsolelightrotaryxrot - 50f, 0f, 180f);
                    consoleprilightsrotary.localRotation = Quaternion.Euler(priconsolelightrotaryxrot, -69.55901f, 90f);
                    if (priconsolelightrotaryxrot > 0){
                        foreach (TextMeshPro cockpittmp in cockpittmps){
                            Color32 col1 = new Color(0f, (priconsolelightrotaryxrot*(51/36))/255f, (priconsolelightrotaryxrot*(29/36))/255f);
                            cockpittmp.color = col1;
                        }   
                    } else {
                        foreach (TextMeshPro cockpittmp in cockpittmps){
                            Color32 col2 = new Color(1f, 1f,1f);
                            cockpittmp.color = col2;
                        } 
                    }
                }

                if (rayHit1.collider.tag == "fuelgaugemoderotary"){
                    fuelgaugedispstate = Mathf.Clamp(fuelgaugedispstate - 1, 0, 5);
                    fuelgaugemoderotary.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if (rayHit1.collider.tag == "hudspddispswitch"){
                    hudairspeedswitchpos = Mathf.Clamp(hudairspeedswitchpos - 1, 0, 2);
                    if(hudairspeedswitchpos == 1){
                        hudairspeedswitch.localRotation = Quaternion.Euler(23.758f, 0f, 0f);
                    }
                    if(hudairspeedswitchpos == 0){
                        hudairspeedswitch.localRotation = Quaternion.Euler(-0.397f, 0f, 0f);
                    }
                    if(hudairspeedswitchpos == 2){
                        hudairspeedswitch.localRotation = Quaternion.Euler(50.047f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "hudattfpmswitch"){
                    if (hudfpmattswitchpos == 2){
                        hudfpmattswitchpos = 1;
                        hudfpmattswitch.localRotation = Quaternion.Euler(21.093f, 0f, 0f);
                        altladder.gameObject.SetActive(false);
                    } else {
                        hudfpmattswitchpos = 2;
                        hudfpmattswitch.localRotation = Quaternion.Euler(37.75f, 0f, 0f);
                        altladder.gameObject.SetActive(true);
                    }
                }

                if (rayHit1.collider.tag == "hudaltrdrswitch"){
                    if (hudaltstate == 2){
                        hudaltstate = 1;
                        hudaltswitch.localRotation = Quaternion.Euler(20.839f, 0f, 0f);
                    } else if(hudaltstate == 1){
                        hudaltstate = 0;
                        hudaltswitch.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "jetfuelswitch"){
                    if (jetfuelstart1 == true && jetfueloff == false && jetfuelstart2 == false){
                        jetfuelstart1 = false;
                        jetfuelstart2 = true;
                        jetfueloff = false;
                        jfs.localRotation = Quaternion.Euler(-0.719f, 0f, 0f);
                    } else if (jetfuelstart2 == true && rpmangle >= -70f){
                        jetfuelstart1 = false;
                        jetfueloff = false;
                        jetfuelstart2 = false;
                        jfs.localRotation = Quaternion.Euler(18.384f, 0f, 0f);
                    } else if (jetfueloff == true){
                        jetfuelstart1 = false;
                        jetfueloff = false;
                        jetfuelstart2 = true;
                        jfs.localRotation = Quaternion.Euler(-0.719f, 0f, 0f);
                    } else if(jetfuelstart2 == true && rpmangle < -70f){
                        jetfuelstart1 = false;
                        jetfueloff = true;
                        jetfuelstart2 = false;
                        jfs.localRotation = Quaternion.Euler(18.384f, 0f, 0f);
                    }
                }

                if (rayHit1.collider.tag == "masterarmswitch"){
                    if (masterarmstate == 0){
                        masterarmstate = 2;
                        masterarmswitch.localRotation = Quaternion.Euler(0f, -9.532001f, 0f);
                        hudarm.text = "SIM".ToString();
                    } else if(masterarmstate == 2){
                        masterarmstate = 0;
                        masterarmswitch.localRotation = Quaternion.Euler(0f, 10.231f, 0f);
                        hudarm.text = " ".ToString();
                    } else{
                        masterarmstate = 2;
                        masterarmswitch.localRotation = Quaternion.Euler(0f, -9.532001f, 0f);
                        hudarm.text = "SIM".ToString();
                    }
                }

                if (rayHit1.collider.tag == "consolefloodset"  && mainpoweron == true){
                    consolefloodlightsrotaryxrotation = Mathf.Clamp(consolefloodlightsrotaryxrotation - 50f, 0f, 180f);
                    foreach (Light conlight in consolefloodlights){
                        conlight.intensity = consolefloodlightsrotaryxrotation/20f;
                    }
                    consolefloodlightsrotary.localRotation = Quaternion.Euler(-consolefloodlightsrotaryxrotation, 110.441f, 89.005f);
                }

                if (rayHit1.collider.tag == "instprilights" && mainpoweron == true){
                    priinstlightsrotaryxrotation = Mathf.Clamp(priinstlightsrotaryxrotation - 50f, 0f, 180f);
                    foreach (Light priinstlight in priinstlights){
                        priinstlight.intensity = priinstlightsrotaryxrotation * 5.55555556f;
                    }
                    priinstlightsrotary.localRotation = Quaternion.Euler(priinstlightsrotaryxrotation, -68.938f, -88.93201f);
                }
            }
            

            var selection = rayHit1.collider;
            var selectionrenderer = selection.GetComponent<Renderer>();
            selectionrenderer.material.SetColor("_Color", Color.red);
            _selection = selection;
        
        }
    }

    void CamControl(){
        if (Input.GetKeyUp(KeyCode.C)){
            if (currentcamera == camcount){
                currentcamera = 0;
            } else {
                currentcamera += 1;
            }
        }
        if (viewcams[currentcamera].enabled != true && missioncam.enabled == false){
            viewcams[currentcamera].enabled = true;
        }
        if (currentcamera != 0 && viewcams[currentcamera - 1].enabled != false){
            viewcams[currentcamera - 1].enabled = false;
        }
        if (currentcamera == 0 && viewcams[camcount - 1].enabled != false){
            viewcams[camcount - 1].enabled = false;
        }
    }

    void getHeight(){
        
        if (Physics.Raycast (tirefront.position, -Vector3.up, out hit)) {
            currentheightfromterrain = hit.distance;
        }

        if (currentheightfromterrain <= 1.5f && currentheightfromterrain >= -0.3f){
            isgrounded = true;
        } else{
            isgrounded = false;
        }
    }

    void CheckThrottle(){

        if (mainpoweron == true && jetfueloff != true && currentfuelleft > 0f){
            if (taximode == false){
                if (Input.GetKey(KeyCode.Alpha4)){
                    throttleidle = false;
                    throttlezero = true;
                    throttleslider.value = 0f;
                    throttlecockpit.localRotation = Quaternion.Euler(0f, 0f, 20.231f);
                }
                if (Input.GetKey(KeyCode.Alpha5)){
                    throttleslider.value = 0f;
                    throttlezero = false;
                    throttleidle = true;
                }

                if (Input.GetKey(KeyCode.Alpha6) && rpm60orabove == true){
                    throttleslider.value = (10f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }

                if (Input.GetKey(KeyCode.Alpha7) && rpm60orabove == true){
                    throttleslider.value = (30f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }
                if (Input.GetKey(KeyCode.Alpha8) && rpm60orabove == true){
                    throttleslider.value = (50f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }

                if (Input.GetKey(KeyCode.Alpha9) && rpm60orabove == true){
                    throttleslider.value = (80f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }

                if (Input.GetKey(KeyCode.Alpha0) && rpm60orabove == true){
                    throttleslider.value = (100f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }
            }
            if (taximode == true && currentfuelleft > 0){
                if (Input.GetKey(KeyCode.Alpha4)){
                    throttleidle = false;
                    throttlezero = true;
                    throttleslider.value = 0f;
                    throttlecockpit.localRotation = Quaternion.Euler(0f, 0f, 20.231f);
                }
                if (Input.GetKey(KeyCode.Alpha5)){
                    throttleslider.value = 0f;
                    throttlezero = false;
                    throttleidle = true;
                }

                if (Input.GetKey(KeyCode.Alpha6) && rpm60orabove == true){
                    throttleslider.value = (0.3f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }

                if (Input.GetKey(KeyCode.Alpha7) && rpm60orabove == true){
                    throttleslider.value = (0.5f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }
                if (Input.GetKey(KeyCode.Alpha8) && rpm60orabove == true){
                    throttleslider.value = (1f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }

                if (Input.GetKey(KeyCode.Alpha9) && rpm60orabove == true){
                    throttleslider.value = (1.5f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }

                if (Input.GetKey(KeyCode.Alpha0) && rpm60orabove == true){
                    throttleslider.value = (2f/100f) * throttleslider.maxValue;
                    throttlezero = false;
                    throttleidle = false;
                }
            }
            
            if ( throttlezero == false){
                throttle = (throttleslider.value/100f) * maxenginepower;
                if (currentenginepower > throttle){
                    currentenginepower = Mathf.Clamp(currentenginepower - 200*Time.deltaTime, 0f, maxenginepower);
                }
                if (currentenginepower < throttle && throttle != maxenginepower){
                    currentenginepower = Mathf.Clamp(currentenginepower + 30*Time.deltaTime, 0f, maxenginepower);
                } else if(currentenginepower < throttle && throttle == maxenginepower){
                    currentenginepower = Mathf.Clamp(currentenginepower + 50*Time.deltaTime, 0f, maxenginepower);
                }
            }

            if (currentfuelleft <= 0f){
                throttle = 0f;
            }
        }
    }

    
    void EngineForce(){
        if((throttle/maxenginepower) * 100f < 90f){
            finalforce = transform.forward * currentenginepower;
        } else{
            finalforce = transform.forward * currentenginepower * 1.5f;
        }
        if (mainpoweron == true && jetfueloff == false && rpm60orabove == true){
            rb.AddForce(finalforce);
        }
    }

    void DragForce(){
        
        if (isgrounded == true && Input.GetKey(KeyCode.B) == false){
            currentdrag = 0.7f + Mathf.Clamp(currentflap, 0.01f, 1f);
        }
        else if(isgrounded == false && Input.GetKey(KeyCode.B) == false) {
            currentdrag = 2f + Mathf.Clamp(currentflap, 0.01f, 1f);
        }
        else if(Input.GetKey(KeyCode.B) && isgrounded == true){
            currentdrag = Mathf.Lerp(currentdrag, 1f, Time.deltaTime * 1f);
        }
        else if(Input.GetKey(KeyCode.B) && isgrounded == false){
            currentdrag = Mathf.Lerp(currentdrag, 3f, Time.deltaTime * 1f);
        }
        if(parkingbreak == false){
            rb.drag = currentdrag;
        } else if(parkingbreak == true){
            rb.drag = currentdrag + 2f;
        }
    }

    void GroundControls(){
        // if (currentheightfromterrain <= 0f ){
        //       transform.position = new Vector3(
        //         transform.position.x,
        //         yHeight + geardistancefromground,
        //         transform.position.z
        //     );
        // }
        
    }

    void Yawcontrol(){
        if (isgrounded == true){
            if (taximode == true){
                yawrate = 7f * currentenginepower;   
            } else {
                yawrate = 1.5f * currentenginepower;
            }
        }
        else {
            yawrate = 0.1f * currentenginepower;
        }
        if (Input.GetKey(KeyCode.Delete)){
            Vector3 torqueforce = transform.up * -yawrate;
            rb.AddTorque(torqueforce);
            yawingleft = true;
            yawingright = false;
        }
        if (Input.GetKey(KeyCode.PageDown)){
            Vector3 torqueforce = transform.up * yawrate;
            rb.AddTorque(torqueforce);
            yawingright = true;
            yawingleft = false;
        }
        rb.angularVelocity = Vector3.zero;
    }

    void CheckLift(){
        Vector3 Liftdirection = transform.up;
        liftPower = 0.5f * currentairspeed * maxLiftPower * Mathf.Clamp(currentflap/50, 1f, 2f);
        finalLiftForce = Liftdirection * liftPower;
        rb.AddForce(finalLiftForce);
        rb.AddTorque( transform.right * currentflap/2 );
    }

    void CheckRoll(){
        if (isgrounded == false){
                rb.AddTorque( transform.forward * -Input.GetAxis("Horizontal") * turnRate );
                Vector3 rolltorqueforce = transform.up * yawrate * Input.GetAxis("Horizontal");
                rb.AddTorque( rolltorqueforce );
        } else if(isgrounded == true){
            Vector3 rolltorqueforce = transform.up * yawrate * Input.GetAxis("Horizontal");
            rb.AddTorque( rolltorqueforce );
        }
    }

    void CheckPitch(){

        if (isgrounded == false){
            rb.AddTorque( transform.right * Input.GetAxis("Vertical") * Mathf.Clamp(currentairspeed * 4f  * currentflap * 0.01f, 200f, 550f));
            }
        else if(isgrounded == true && CAS > 120f){
            rb.AddTorque( transform.right * Input.GetAxis("Vertical") * Mathf.Clamp(currentairspeed * 4f  * currentflap * 0.01f, 200f, 550f));
        }

    }

    void CheckFlap(){

        if (mainpoweron == true){
            if(Input.GetKey(KeyCode.L)){
                flapslider.value -= (30f/100f) * flapslider.maxValue * Time.deltaTime;
            }

            if(Input.GetKey(KeyCode.F)){
                flapslider.value += (30f/100f) * flapslider.maxValue * Time.deltaTime;
            }

            currentflap = 1/flapslider.value;
        }
    }

    void CheckControlSurfaces(){
        
        if (mainpoweron == true){

            if (yawingleft == true){
                rudder.localRotation = Quaternion.Euler(-31.415f, 29f, -13f);
            }
            if (yawingright == true){
                rudder.localRotation = Quaternion.Euler(-26.415f, -29f, 13f);
            }
            if (yawingleft == false && yawingright == false){
                rudder.localRotation = Quaternion.Euler(-32.415f, 0f, 0f);
            }
            if (pitchingdown == true){
                rightelevator.localRotation = Quaternion.Euler(4f, 0f, 0f);
                leftelevator.localRotation = Quaternion.Euler(4f, 180f, 0f);
            }
            if (pitchingup == true){
                rightelevator.localRotation = Quaternion.Euler(-4f, 0f, 0f);
                leftelevator.localRotation = Quaternion.Euler(-4f, 180f, 0f);
            }
            if (pitchingup == false && pitchingdown == false){
                rightelevator.localRotation = Quaternion.Euler(0f, 0f, 0f);
                leftelevator.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            if (rollingleft == true){
                rightaileron.localRotation = Quaternion.Euler(-6f, 10.527f, 0f);
                leftaileron.localRotation = Quaternion.Euler(6f, -10.527f, 0f);
            }
            if (rollingright == true){
                rightaileron.localRotation = Quaternion.Euler(6f, 10.527f, 0f);
                leftaileron.localRotation = Quaternion.Euler(-6f, -10.527f, 0f);
            }
            if (rollingright == false && rollingleft == false){
                rightaileron.localRotation = Quaternion.Euler(0f, 10.527f, 0f);
                leftaileron.localRotation = Quaternion.Euler(0f, -10.527f, 0f);
            }

        }

    }

    void CheckGear(){

        if (Input.GetKeyUp(KeyCode.G)){
            if(gearup == true){
                gearup = false;
            } else if (gearup == false && isgrounded == false){
                gearup = true;
            }
        }

        if (gearup == true){
            rightgear.localRotation = Quaternion.Euler(37.287f, 151.624f, -60.399f);
            leftgear.localRotation = Quaternion.Euler(-47.396f, 28.03f, -50.35f);
            rightgeardoor.localRotation = Quaternion.Euler(-0.294f, -2.974f, 84.369f);
            leftgeardoor.localRotation = Quaternion.Euler(0.772f, 4.598f, -99.512f);
            frontgear.localRotation = Quaternion.Euler(99.785f, -1.52587f, -1.52587f);
            frontgeardoor.localRotation = Quaternion.Euler(0f, -0.278f, -81.194f);
        }
        if (gearup == false){
            rightgear.localRotation = Quaternion.Euler(0f, 91.741f, 0f);
            leftgear.localRotation = Quaternion.Euler(0f, 91.741f, 0f);
            rightgeardoor.localRotation = Quaternion.Euler(0f, 0f, 0f);
            leftgeardoor.localRotation = Quaternion.Euler(0f, 0f, 0f);
            frontgear.localRotation = Quaternion.Euler(0f, 0f, 0f);
            frontgeardoor.localRotation = Quaternion.Euler(0f, -0.278f, 0f);
        }
    }

    Vector3 ProjectPointOnPlane(Vector3 planeNormal , Vector3 planePoint , Vector3 point ){
        planeNormal.Normalize();
        var distance = -Vector3.Dot(planeNormal.normalized, (point - planePoint));
        return point + planeNormal * distance;
    }    
 
    float SignedAngle(Vector3 v1, Vector3 v2, Vector3 normal){
        var perp = Vector3.Cross(normal, v1);
        var angle = Vector3.Angle(v1, v2);
        angle *= Mathf.Sign(Vector3.Dot(perp, v2));
        return angle;
    }

    void CheckHud(){

        if(huddedswitchstate == 2){
            dedhudshow = true;
        } 
        if(huddedswitchstate == 1){
            dedhudshow = false;
        }
        if(huddedswitchstate == 0){
            dedhudshow = false;
        }

        Vector3 pos;
        float roll;
        float pitch;

        pos = ProjectPointOnPlane(Vector3.up, Vector3.zero, transform.right);
        roll = SignedAngle(transform.right, pos, transform.forward);

        pos = ProjectPointOnPlane(Vector3.up, Vector3.zero, transform.forward);
        pitch = SignedAngle(transform.forward, pos, transform.right);
        CAS = (TAS - pitch) * 0.9f;
       if (mainpoweron == true && rpm60orabove == true){
           hudcomps.SetActive(true);

            if ( pitch <= 10f && pitch >= -10f ){
                hudarhordotteddown1.gameObject.SetActive(true);
                hudarhordotteddown2.gameObject.SetActive(true);
                hudarhorsolidup1.gameObject.SetActive(true);
                hudarhorsolidup2.gameObject.SetActive(true);
                hudarhorsoliddown1.gameObject.SetActive(false);
                hudarhorsoliddown2.gameObject.SetActive(false);
                hudarhordottedup1.gameObject.SetActive(false);
                hudarhordottedup2.gameObject.SetActive(false);
            }

            if ( pitch < -10f && pitch > -25f ){
                hudarhordotteddown1.gameObject.SetActive(true);
                hudarhordotteddown2.gameObject.SetActive(true);
                hudarhorsolidup1.gameObject.SetActive(true);
                hudarhorsolidup2.gameObject.SetActive(false);
                hudarhorsoliddown1.gameObject.SetActive(false);
                hudarhorsoliddown2.gameObject.SetActive(false);
                hudarhordottedup1.gameObject.SetActive(false);
                hudarhordottedup2.gameObject.SetActive(true);
            }

            if ( pitch < -25f ){
                hudarhordotteddown1.gameObject.SetActive(true);
                hudarhordotteddown2.gameObject.SetActive(true);
                hudarhorsolidup1.gameObject.SetActive(false);
                hudarhorsolidup2.gameObject.SetActive(false);
                hudarhorsoliddown1.gameObject.SetActive(false);
                hudarhorsoliddown2.gameObject.SetActive(false);
                hudarhordottedup1.gameObject.SetActive(true);
                hudarhordottedup2.gameObject.SetActive(true);
            }

            if ( pitch > 10f && pitch < 25f ){
                hudarhordotteddown1.gameObject.SetActive(true);
                hudarhordotteddown2.gameObject.SetActive(false);
                hudarhorsolidup1.gameObject.SetActive(true);
                hudarhorsolidup2.gameObject.SetActive(true);
                hudarhorsoliddown1.gameObject.SetActive(false);
                hudarhorsoliddown2.gameObject.SetActive(true);
                hudarhordottedup1.gameObject.SetActive(false);
                hudarhordottedup2.gameObject.SetActive(false);
            }

            if ( pitch > 25f){
                hudarhordotteddown1.gameObject.SetActive(false);
                hudarhordotteddown2.gameObject.SetActive(false);
                hudarhorsolidup1.gameObject.SetActive(true);
                hudarhorsolidup2.gameObject.SetActive(true);
                hudarhorsoliddown1.gameObject.SetActive(true);
                hudarhorsoliddown2.gameObject.SetActive(true);
                hudarhordottedup1.gameObject.SetActive(false);
                hudarhordottedup2.gameObject.SetActive(false);
            }

            arhorhud.localRotation = Quaternion.Euler(0f, -roll - 2f, 0f);
            arhor.localRotation = Quaternion.Euler(0f, -roll - 2f, 0f);

            hudpitch11.text = ((Mathf.Round(pitch/5f) * 5f) + 10f).ToString("F0");
            hudpitch12.text = ((Mathf.Round(pitch/5f) * 5f) + 10f).ToString("F0");
            hudpitch21.text = ((Mathf.Round(pitch/5f) * 5f) + 5f).ToString("F0");
            hudpitch22.text = ((Mathf.Round(pitch/5f) * 5f) + 5f).ToString("F0");
            hudpitch31.text = ((Mathf.Round(pitch/5f) * 5f) - 5f).ToString("F0");
            hudpitch32.text = ((Mathf.Round(pitch/5f) * 5f) - 5f).ToString("F0");
            hudpitch41.text = ((Mathf.Round(pitch/5f) * 5f) - 10f).ToString("F0");
            hudpitch42.text = ((Mathf.Round(pitch/5f) * 5f) - 10f).ToString("F0");

            if (hudaltstate == 2){
                hudaltitude.text = currentheightfromterrain.ToString("F2");
                if ( Mathf.Floor(currentheightfromterrain /100f)*10f <= 0f){
                    altladder3.text = "".ToString();
                } else {
                    altladder3.text = (Mathf.Floor(currentheightfromterrain /100f)*10f).ToString("F0");
                }
                if ( (Mathf.Floor(currentheightfromterrain /100f)*10f) + 5f <= 0f){
                    altladder2.text = "".ToString();
                } else {
                    altladder2.text = ((Mathf.Floor(currentheightfromterrain /100f)*10f) + 5f).ToString("F0");
                }
                if ( (Mathf.Floor(currentheightfromterrain /100f)*10f) + 10f <= 0f){
                    altladder1.text = "".ToString();
                } else {
                    altladder1.text = ((Mathf.Floor(currentheightfromterrain /100f)*10f) + 10f).ToString("F0");
                }
                if ( (Mathf.Floor(currentheightfromterrain /100f)*10f) - 5f <= 0f){
                    altladder4.text = "".ToString();
                } else {
                    altladder4.text = ((Mathf.Floor(currentheightfromterrain /100f)*10f) - 5f).ToString("F0");
                }
            } else if(hudaltstate == 1){
                hudaltitude.text = transform.position.y.ToString("F2");
                if ( Mathf.Floor(transform.position.y/100f)*10f <= 0f){
                    altladder3.text = "".ToString();
                } else {
                    altladder3.text = (Mathf.Floor(transform.position.y /100f)*10f).ToString("F0");
                }
                if ( (Mathf.Floor(transform.position.y /100f)*10f) + 5f <= 0f){
                    altladder2.text = "".ToString();
                } else {
                    altladder2.text = ((Mathf.Floor(transform.position.y /100f)*10f) + 5f).ToString("F0");
                }
                if ( (Mathf.Floor(transform.position.y /100f)*10f) + 10f <= 0f){
                    altladder1.text = "".ToString();
                } else {
                    altladder1.text = ((Mathf.Floor(transform.position.y /100f)*10f) + 10f).ToString("F0");
                }
                if ( (Mathf.Floor(transform.position.y /100f)*10f) - 5f <= 0f){
                    altladder4.text = "".ToString();
                } else {
                    altladder4.text = ((Mathf.Floor(transform.position.y /100f)*10f) - 5f).ToString("F0");
                }
            } else if( hudaltstate == 0){
                hudaltitude.text = " ".ToString();
            }
            altpointer.localRotation = Quaternion.Euler(0f, -90 + (currentheightfromterrain/33.333f), 0f);
            if (hudairspeedswitchpos == 0){
                hudspeed.text = currentairspeed.ToString("F0");
                if ( Mathf.Floor(currentairspeed/100f)*10f <= 0f){
                    speedladder3.text = "".ToString();
                } else {
                    speedladder3.text = (Mathf.Floor(currentairspeed/100f)*10f).ToString("F0");
                }
                if ( (Mathf.Floor(currentairspeed/100f)*10f) + 5f <= 0f){
                    speedladder2.text = "".ToString();
                } else {
                    speedladder2.text = ((Mathf.Floor(currentairspeed/100f)*10f) + 5f).ToString("F0");
                }
                if ( (Mathf.Floor(currentairspeed/100f)*10f) + 10f <= 0f){
                    speedladder1.text = "".ToString();
                } else {
                    speedladder1.text = ((Mathf.Floor(currentairspeed/100f)*10f) + 10f).ToString("F0");
                }
                if ( (Mathf.Floor(currentairspeed/100f)*10f) - 5f <= 0f){
                    speedladder4.text = "".ToString();
                } else {
                    speedladder4.text = ((Mathf.Floor(currentairspeed/100f)*10f) - 5f).ToString("F0");
                }
            } else if (hudairspeedswitchpos == 1){
                hudspeed.text = TAS.ToString("F0");
                if ( Mathf.Floor(TAS/100f)*10f <= 0f){
                    speedladder3.text = "".ToString();
                } else {
                    speedladder3.text = (Mathf.Floor(TAS/100f)*10f).ToString("F0");
                }
                if ( (Mathf.Floor(TAS/100f)*10f) + 5f <= 0f){
                    speedladder2.text = "".ToString();
                } else {
                    speedladder2.text = ((Mathf.Floor(TAS/100f)*10f) + 5f).ToString("F0");
                }
                if ( (Mathf.Floor(TAS/100f)*10f) + 10f <= 0f){
                    speedladder1.text = "".ToString();
                } else {
                    speedladder1.text = ((Mathf.Floor(TAS/100f)*10f) + 10f).ToString("F0");
                }
                if ( (Mathf.Floor(TAS/100f)*10f) - 5f <= 0f){
                    speedladder4.text = "".ToString();
                } else {
                    speedladder4.text = ((Mathf.Floor(TAS/100f)*10f) - 5f).ToString("F0");
                }
            } else if (hudairspeedswitchpos == 2){
                hudspeed.text = CAS.ToString("F0");
                if ( Mathf.Floor(CAS/100f)*10f <= 0f){
                    speedladder3.text = "".ToString();
                } else {
                    speedladder3.text = (Mathf.Floor(CAS/100f)*10f).ToString("F0");
                }
                if ( (Mathf.Floor(CAS/100f)*10f) + 5f <= 0f){
                    speedladder2.text = "".ToString();
                } else {
                    speedladder2.text = ((Mathf.Floor(CAS/100f)*10f) + 5f).ToString("F0");
                }
                if ( (Mathf.Floor(CAS/100f)*10f) + 10f <= 0f){
                    speedladder1.text = "".ToString();
                } else {
                    speedladder1.text = ((Mathf.Floor(CAS/100f)*10f) + 10f).ToString("F0");
                }
                if ( (Mathf.Floor(CAS/100f)*10f) - 5f <= 0f){
                    speedladder4.text = "".ToString();
                } else {
                    speedladder4.text = ((Mathf.Floor(CAS/100f)*10f) - 5f).ToString("F0");
                }
            }
            
            machval.text = currentmach.ToString("F2");
            machvalhud.text = currentmach.ToString("F2");
            machpointer.localRotation = Quaternion.Euler(0f, -90 + (currentmach * 174f), 0f);

            heading = Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg + Mathf.Clamp(headingcorrection, -180f, 180f);
            if (heading <= 1f && heading >= -1f){
                hudheadingval.text = heading.ToString("F2") + "N";
            }
            if (heading <= 91f && heading >= 89f){
                hudheadingval.text = heading.ToString("F2") + "W";
            }
            if (heading <= -91f && heading >= -89f){
                hudheadingval.text = heading.ToString("F2") + "E";
            }
            if (heading <= -179f && heading >= 179f){
                hudheadingval.text = heading.ToString("F2") + "S";
            }
            if (heading < 89f && heading > 1f){
                hudheadingval.text = heading.ToString("F2") + "NW";
            }
            if (heading < -1f && heading > -89f){
                hudheadingval.text = heading.ToString("F2") + "NE";
            }
            if (heading < 179f && heading > 91f){
                hudheadingval.text = heading.ToString("F2") + "SW";
            }
            if (heading < -91f && heading > -179f){
                hudheadingval.text = heading.ToString("F2") + "SE";
            }

            cockpitcompasspointer.localRotation = Quaternion.Euler(0f, -90f + heading, 0f);
            
            if (gearup == true && gearstatehud.enabled == true){
                gearstatehud.enabled = false;
            }
            if (gearup == false && gearstatehud.enabled == false){
                gearstatehud.enabled = true;
            }

            throttlevalhud.text = ((throttle/maxenginepower) * 100f).ToString("F1");
            hudcurrentstpt.text = currentstpt.ToString();
            huddist2currentstpt.text = (distancetostpt/1000f).ToString("F1");
            if (mainpoweron == true && ufcpoweron == true && rpm60orabove == true && dedhudshow == true){
                if (currentdedpage == "cni"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[0].SetActive(true);
                } else if (currentdedpage == "com1"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[1].SetActive(true);

                } else if (currentdedpage == "com2"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[2].SetActive(true);
                    
                } else if (currentdedpage == "tils"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[3].SetActive(true);
                } else if (currentdedpage == "alow"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[4].SetActive(true);
                } else if (currentdedpage == "stpt"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[5].SetActive(true);

                } else if (currentdedpage == "time"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[6].SetActive(true);
                    
                } else if (currentdedpage == "list"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[7].SetActive(true);

                } else if (currentdedpage == "bingo"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[8].SetActive(true);
                    
                } else if (currentdedpage == "navstat"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[9].SetActive(true);
                    
                } else if (currentdedpage == "navcomms"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[10].SetActive(true);
                    
                } else if (currentdedpage == "man"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[11].SetActive(true);
                } else if (currentdedpage == "ins"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[12].SetActive(true);
                } else if (currentdedpage == "dlink1"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[13].SetActive(true);
                } else if (currentdedpage == "dlink2"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[14].SetActive(true);
                } else if (currentdedpage == "dlink3"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[15].SetActive(true);
                } else if (currentdedpage == "cmds1"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[16].SetActive(true);
                } else if (currentdedpage == "cmds2"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[17].SetActive(true);
                } else if (currentdedpage == "cmds3"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[18].SetActive(true);
                } else if (currentdedpage == "misc"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[19].SetActive(true);
                    
                } else if (currentdedpage == "mode"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[20].SetActive(true);
                } else if (currentdedpage == "magv"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[21].SetActive(true);
                    
                } else if (currentdedpage == "lasr"){
                    foreach(GameObject dedpage in dedpageshud){
                        dedpage.SetActive(false);
                    }
                    dedpageshud[22].SetActive(true);
                
            } else {
                foreach(GameObject dedpage in dedpageshud){
                    dedpage.SetActive(false);
                }   
            } 
            dedcnistptvalhud.text = currentstpt.ToString();
            dedcniuhfvalhud.text = currentuhf.ToString("F2");
            dedcnivhfvalhud.text = currentvhf.ToString("F2");
            uhffreqvaldedset1hud.text = currentuhf.ToString("F2");
            vhffreqvaldedset1hud.text = currentvhf.ToString("F2");
            bingocurrentfuellefthud.text = currentfuelleft.ToString("F0");
            magvmodehud.text = magvmode1;
            magvvalhud.text = magvval1;
            magvdirhud.text = magvdir1;
            egifiltermodehud.text = egifiltermode1;
            stptpagemodehud.text = currentstptmode;
            stptpagestptvalhud.text = currentstpt.ToString();
            dedcnitimehud.text = dedcnitime.text.ToString();
            uhffreqvaldedsethud.text = uhffreqvaldedset.text.ToString();
            vhffreqvaldedsethud.text = vhffreqvaldedset.text.ToString();
            bingosethud.text = bingoset.text.ToString();
            hacktimetmptpagehud.text = hacktimetmptpage.text.ToString();
            hacktimetmpcnipagehud.text = hacktimetmpcnipage.text.ToString();
            
            }if(dedhudshow == false) {
                foreach(GameObject dedpage in dedpageshud){
                    dedpage.SetActive(false);
                } 
            }

       } else if (mainpoweron == false || rpm60orabove == false){
           hudcomps.SetActive(false);
       }

    }

    void CheckcockpitLights(){
        if (mainpoweron == true){
            if (gearup == false && jetfueloff == false && rpm60orabove == true && frontgearlight.GetComponent<Renderer>().material.color != Color.green && rightgearlight.GetComponent<Renderer>().material.color != Color.green && leftgearlight.GetComponent<Renderer>().material.color != Color.green){
                leftgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                rightgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                frontgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            } else if (gearup == true && jetfueloff == false && rpm60orabove == true && frontgearlight.GetComponent<Renderer>().material.color != Color.red && rightgearlight.GetComponent<Renderer>().material.color != Color.red && leftgearlight.GetComponent<Renderer>().material.color != Color.red){
                leftgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                rightgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                frontgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            } else if(mainpoweron == false || rpm60orabove == false || jetfueloff == true){
                leftgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                rightgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                frontgearlight.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }

            if (masterlightrotstate == 1 && rpm60orabove == true){
                masterlightrot.localRotation = Quaternion.Euler(0f, 169.623f, 89.339f);
                if (acollrotstate == 0){
                    acollrot.localRotation = Quaternion.Euler(-4.746f, 89.395f, 84.941f);
                    foreach(Light acolllight in anticollisionlights){
                        acolllight.enabled = false;
                    }
                    StopCoroutine("blinkaclights");
                } 
                if (acollrotstate == 1){
                    acollrot.localRotation = Quaternion.Euler(-64.355f, 258.388f, -78.285f);
                    if (aclightisblinking == false){
                        StartCoroutine("blinkaclights");
                    }
                } else if(acollrotstate == 2){
                    acollrot.localRotation = Quaternion.Euler(-30.542f, 265.992f, -84.144f);
                    if (aclightisblinking == false){
                        StartCoroutine("blinkaclights");
                    }
                } else if(acollrotstate == 3){
                    acollrot.localRotation = Quaternion.Euler(6.17f, 269.522f, -84.929f);
                    if (aclightisblinking == false){
                        StartCoroutine("blinkaclights");
                    }
                } else if(acollrotstate == 4){
                    acollrot.localRotation = Quaternion.Euler(30.22f, 271.921f, -84.163f);
                    if (aclightisblinking == false){
                        StartCoroutine("blinkaclights");
                    }
                } else if (acollrotstate == 5){
                    acollrot.localRotation = Quaternion.Euler(58.028f, 277.1f, -80.44701f);
                    if (aclightisblinking == false){
                        StartCoroutine("blinkaclights");
                    }
                } else if(acollrotstate == 6){
                    acollrot.localRotation = Quaternion.Euler(83.93f, 392.918f, 33.793f);
                    if (aclightisblinking == false){
                        StartCoroutine("blinkaclights");
                    }
                }
                if (poslightfsswitchstate == 1){
                    poslightfsswitch.localRotation = Quaternion.Euler(29.009f, 0f, 0f);
                    foreach (Light poslight in wtlights){
                        poslight.enabled = false;
                    }
                    StartCoroutine("blinkposlights");
                } else if( poslightfsswitchstate == 0){
                    poslightfsswitch.localRotation = Quaternion.Euler(-15.447f, 0f, 0f);
                    StopCoroutine("blinkposlights");
                    foreach (Light poslight in wtlights){
                        poslight.enabled = true;
                    }
                }
                flightrot.localRotation = Quaternion.Euler(0f, 0f, flightrotstate);
                foreach (Light flight in flights){
                    flight.intensity = flightrotstate * (3f/260f);
                }
                foreach(Light arflight in refuelinglights){
                    arflight.intensity = arfuelingrotstate * (1f/27f);
                }
                arfuelingrot.localRotation = Quaternion.Euler(-546.215f, -199.069f, 90 + arfuelingrotstate);

            } else if(masterlightrotstate == 0){
                masterlightrot.localRotation = Quaternion.Euler(0f, 169.623f, 265.041f);
                foreach (Light poslight in wtlights){
                    poslight.enabled = false;
                }
                foreach (Light flagelight in flagelights){
                    flagelight.enabled = false;
                }
                foreach(Light arflight in refuelinglights){
                    arflight.intensity = 0f;
                }
            }

            if (wtbrtswitchstate == 0){
                wtbrtswitch.localRotation = Quaternion.Euler(17.456f, 0f, 0f);
                foreach (Light poslight in wtlights){
                    poslight.intensity = 0f;
                }
            } else if (wtbrtswitchstate == 1){
                wtbrtswitch.localRotation = Quaternion.Euler(44.386f, 0f, 0f);
                foreach (Light poslight in wtlights){
                    poslight.intensity = 3f;
                }
            } else if(wtbrtswitchstate == 2){
                wtbrtswitch.localRotation = Quaternion.Euler(-13.267f, 0f, 0f);
                foreach (Light poslight in wtlights){
                    poslight.intensity = 1f;
                }
            }
            if (flagebrtswitchstate == 0){
                flagebrtswitch.localRotation = Quaternion.Euler(21.455f, 0f, 0f);
                foreach (Light flagelight in flagelights){
                    flagelight.enabled = false;
                    flagelight.intensity = 0f;
                }
            } else if (flagebrtswitchstate == 1){
                flagebrtswitch.localRotation = Quaternion.Euler(45.95f, 0f, 0f);
                foreach (Light flagelight in flagelights){
                    flagelight.enabled = true;
                    flagelight.intensity = 3f;
                }
            } else if(flagebrtswitchstate == 2){
                flagebrtswitch.localRotation = Quaternion.Euler(-12.511f, 0f, 0f);
                foreach (Light flagelight in flagelights){
                    flagelight.enabled = true;
                    flagelight.intensity = 1f;
                }
            }
        }
        
    }

    IEnumerator blinkaclights(){
        while (true){
            aclightisblinking = true;
            if(acollrotstate != 0){
                yield return new WaitForSeconds(4/(acollrotstate));
                foreach(Light aclight in anticollisionlights){
                    if (aclight.enabled == true){
                        aclight.enabled = false;
                    } else{
                        aclight.enabled = true;
                    }
                }
            } else {
                foreach(Light aclight in anticollisionlights){
                    aclight.enabled = true;
                }
            }
        }
        aclightisblinking = false;
    }

    IEnumerator blinkposlights(){
        yield return new WaitForSeconds(0.7f);
        foreach ( Light poslight in wtlights ){
            poslight.enabled = true;
        }
        yield return new WaitForSeconds(1f);
        foreach ( Light poslight in wtlights ){
            poslight.enabled = false;
        }
    }

    void CheckcockpitInputs(){

        if (mainpoweron == true){
            if (throttlezero == false){
                throttlecockpit.localRotation = Quaternion.Euler(0f, 0f, (throttleslider.value/100f) * -40f);
            }
            controlstickcockpit.localRotation = Quaternion.Euler(-Input.GetAxis("Horizontal") * 5f, 0f, -Input.GetAxis("Vertical") * 5f);
            landinggearleverdown = Quaternion.Euler(50.043f, -12.656f, -19.033f);
            landinggearleverup = Quaternion.Euler(77.89301f, -91.01401f, -93.09901f);
            if (gearup == true && landinggearlever.localRotation != landinggearleverup){
                landinggearlever.localRotation = landinggearleverup;
            }
            if (gearup == false && landinggearlever.localRotation != landinggearleverdown){
                landinggearlever.localRotation = landinggearleverdown;
            }
            nozpospointer.localRotation = Quaternion.Euler(0f, -37f + ((throttle/maxenginepower) * 250f), 0f);
            if (mainpoweron == true && jetfueloff == false && throttlezero == false && throttleidle == true && rpmmeterpointer.rotation.y != 15f){
                rpmangle = 15f;
                rpmmeterpointer.localRotation = Quaternion.RotateTowards(rpmmeterpointer.localRotation, Quaternion.Euler(0f, rpmangle, 0f), 4.4f * Time.deltaTime);
            }
            if(throttlezero == true && mainpoweron == true && throttleidle == false && jetfueloff == false && rpmmeterpointer.rotation.y != -70f){
                rpmangle = -70;
                rpmmeterpointer.localRotation = Quaternion.RotateTowards(rpmmeterpointer.localRotation, Quaternion.Euler(0f, rpmangle, 0f), 4f * Time.deltaTime);
            }
            if(throttlezero == false && mainpoweron == true && throttleidle == false && jetfueloff == false && rpmmeterpointer.rotation.y >= 15f){
                rpmmeterpointer.localRotation = Quaternion.Euler(0f, rpmmeterpointer.localRotation.y + (currentenginepower/maxenginepower) * 215f, 0f);
            }
            
            if (gearlightswitchstate == 0){
                landinglight.intensity = 0f;
                taxilight.intensity = 0f;
                gearlightswitch.localRotation = Quaternion.Euler(0f, 0f, 0f);
            } else if (gearlightswitchstate == 1 && rpm60orabove == true){
                landinglight.intensity = 30f;
                taxilight.intensity = 0f;
                gearlightswitch.localRotation = Quaternion.Euler(0f, 20.632f, 0f);
            } else if (gearlightswitchstate == 2 && rpm60orabove == true){
                landinglight.intensity = 0f;
                taxilight.intensity = 25f;
                gearlightswitch.localRotation = Quaternion.Euler(0f, -34.286f, 0f);
            }
            icphudbrtrot.localRotation = Quaternion.Euler(icphudbrtrotval, 171.99f, 89.80901f);
            icphudsymrot.localRotation = Quaternion.Euler(icphudsymrotval, 171.99f, 89.80901f);
            icphudcontrot.localRotation = Quaternion.Euler(icphudcontrotval, 171.99f, 89.80901f);
            foreach(SpriteRenderer hudspriterndr in hudcompssprites){
                hudspriterndr.color = new Color(Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * (8f/255f) - (icphudcontrotval/360) * (6.5f/255f), Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) - (icphudcontrotval/360) * (100f/255f), Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * (41f/255f) - (icphudcontrotval/360) * (25f/255f), (icphudsymrotval/360f));
                //hudbrtness = hudspriterndr.color.r * 0.3f + hudspriterndr.color.g * 0.59f + hudspriterndr.color.b * 0.11f;
            }
            foreach(TextMeshPro hudtmp in hudtmps){
                //hudtmp.color = new Color(Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * (8f), Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * 255, Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * (41f), (icphudsymrotval/360f));
                Color32 col34 = new Color((Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * (8f))/100f, (Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f)), (Mathf.Clamp(icphudbrtrotval/360f, 0.5f, 1f) * (15f))/100f, (icphudsymrotval/360f));
                hudtmp.faceColor = col34;
                hudtmp.outlineWidth = icphudcontrotval * (0.3f/360f);
            }  

            if(ufcpoweron == true){
                ufcpowerswitch.localRotation = Quaternion.Euler(41.789f, 0f, 0f);
            } else if(ufcpoweron == false){
                ufcpowerswitch.localRotation = Quaternion.Euler(-12.535f, 0f, 0f);
            }
            if(mfdpoweron == true){
                mfdpowerswitch.localRotation = Quaternion.Euler(41.789f, 0f, 0f);
            } else if(ufcpoweron == false){
                mfdpowerswitch.localRotation = Quaternion.Euler(-12.535f, 0f, 0f);
            }
            if(mmcpoweron == true){
                mmcpowerswitch.localRotation = Quaternion.Euler(41.789f, 0f, 0f);
            } else if(ufcpoweron == false){
                mmcpowerswitch.localRotation = Quaternion.Euler(-12.535f, 0f, 0f);
            }
            if(ststapoweron == true){
                ststapowerswitch.localRotation = Quaternion.Euler(41.789f, 0f, 0f);
            } else if(ufcpoweron == false){
                ststapowerswitch.localRotation = Quaternion.Euler(-12.535f, 0f, 0f);
            }
            if(gpspoweron == true){
                gpspowerswitch.localRotation = Quaternion.Euler(41.789f, 0f, 0f);
            } else if(ufcpoweron == false){
                gpspowerswitch.localRotation = Quaternion.Euler(-12.535f, 0f, 0f);
            }
            if(dlpoweron == true){
                dlpowerswitch.localRotation = Quaternion.Euler(41.789f, 0f, 0f);
            } else if(ufcpoweron == false){
                dlpowerswitch.localRotation = Quaternion.Euler(-12.535f, 0f, 0f);
            }

        }
        if(mainpoweron == false){
            jetfueloff = true;
            if(rpmmeterpointer.localRotation.y != -90f){
                rpmmeterpointer.localRotation = Quaternion.RotateTowards(rpmmeterpointer.localRotation, Quaternion.Euler(0f, -90f, 0f), 4f * Time.deltaTime);
            }
        }
        if(rpmmeterpointer.localRotation.eulerAngles.y >= 290f || rpmmeterpointer.localRotation.eulerAngles.y <= 270f){
            jfs.localRotation = Quaternion.Euler(18.384f, 0f, 0f);
        }
        if(rpmmeterpointer.localRotation.eulerAngles.y >= 15f && rpmmeterpointer.localRotation.eulerAngles.y <= 270f){
            rpm60orabove = true;
        } else{
            rpm60orabove = false;
        }
        Debug.Log(rpmmeterpointer.localRotation.eulerAngles.y);
    }
    

    void CheckStall(){
         
        // if(CAS <= 150f){
        //    rb.AddForce( Vector3.up * -98f );
        // } else if (CAS < 70f){
        //   rb.AddTorque( transform.right * 10f );
        //   rb.AddForce( Vector3.up * -300f );
        // }
    }

    void CheckcockpitClock(){
        
        int secondsInt = int.Parse(System.DateTime.UtcNow.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
        int hoursInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));
        secondhand.localRotation = Quaternion.Euler(0f, -90f + (secondsInt * 6f), 0f);
        minutehand.localRotation = Quaternion.Euler(0f, -90f + (minutesInt * 6f), 0f);
        hourhand.localRotation = Quaternion.Euler(0f, -87.842f + (hoursInt * 30f), 0f);
        Debug.Log(hoursInt + " " + minutesInt + " " + secondsInt);
        dedcnitime.text = System.DateTime.Now.ToString("HH:mm:ss");
        if(hacktimerrunning == true){
            if(hacksec == 59 && hackmin == 59){
                hacksec = 0;
                hackmin = 0;
                hackhour += 1;
            } else if(hacksec == 59){
                hacksec = 0;
                hackmin += 1;
            } else{
                hacksec += 1;
            }
        }
    }

    void CheckFlybycam(){
        if (currentcamera == 4){
            Transform fbcam = viewcams[currentcamera].gameObject.transform;
            float dist = Mathf.Sqrt((transform.position.z - fbcam.position.z) * (transform.position.z - fbcam.position.z) + (transform.position.y - fbcam.position.y) * (transform.position.y - fbcam.position.y));
            Debug.Log(dist);
            if (dist >= 650f || dist <= -500f){
                fbcam.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 500f);
            } else {
                fbcam.LookAt(transform);
            }
        }
    }

}
