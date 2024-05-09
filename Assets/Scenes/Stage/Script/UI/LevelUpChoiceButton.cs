using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpChoiceButton : MonoBehaviour
{
    public enum ButType { LevelUp, Tresure };
    public int choiceNo;
    public LevelUpManager lMng;
    public TresureManager tMng;
    public Text infoText;
    public Text levelText;
    public Image icon;
    public ButType BType;

    Player plScr;
    ImageManager img;
    int iconNo = -1;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickFunc() {
        SeManager.Instance.Play("Equip");

        // 
        if (img.CheckIconNoIsWeapon(iconNo))
        {
            // 武器を選択
            int eNo = plScr.CheckWeaponEquip(iconNo);
            if (eNo != -1) {
                // 装備されている場合
                plScr.LevelUpWeapon(eNo, 1);
            }
            else {
                // 装備を追加
                eNo = plScr.WeaponEquipNum;
                plScr.SetWeaponTbl(eNo, iconNo, 1);
            }
        }
        else {
            // アイテム
            if (img.CheckIconNoIsUseItem(iconNo) ) {
                if (iconNo == (int)IconNo.Money ) {
                    // 金
                    plScr.Money += ItemDefine.MoneyItemAdd;
                }
                else {
                    // 回復アイテム
                    plScr.HealHP(ItemDefine.PotionHealHP);
                }
            }
            else {
                // 装備用
                int eNo = plScr.CheckItemEquip(iconNo);
                if (eNo != -1)
                {
                    // 装備されている場合
                    plScr.LevelUpItem(eNo, 1);
                }
                else
                {
                    // 装備を追加
                    eNo = plScr.ItemEquipNum;
                    plScr.SetItemTbl(eNo, iconNo, 1);
                }
            }
        }

        if (BType == ButType.LevelUp)
        {
            // まだレベルアップが必要かチェック
            if (plScr.CheckLevelUp())
            {
                // 再度レベルアップ
                lMng.SetUp();
            }
            else
            {
                // レベルアップ終了
                StageManager.Ins.EndLevelUp();
            }
        }
        else {
            // 宝箱終了
            StageManager.Ins.EndTresure();
        }
    }

    public void ImageUpdate(int level) {
        if (!plScr)
        {
            plScr = StageManager.Ins.PlScr;
            img = ImageManager.Ins;
        }

        if (BType == ButType.LevelUp)
        {
            // 自信の番号とチェック
            iconNo = lMng.choiceNoTbl[choiceNo];
        }
        else
        {
            // 宝箱用
            iconNo = tMng.ChoiceNo;
        }
        // アイコン設定
        icon.sprite = ImageManager.Ins.GetSprite(iconNo);
        // テキスト設定
        if (textJaggedTbl[iconNo].Length > level)
        {
            if (iconNo == (int)IconNo.MShot) {
                GunSOBJ sObj = WeaponManager.Ins.gunSobj;
                switch (level) {
                    case 1: infoText.text = sObj.string2; break;
                    case 2: infoText.text = sObj.string3; break;
                    case 3: infoText.text = sObj.string4; break;
                    case 4: infoText.text = sObj.string5; break;
                    case 5: infoText.text = sObj.string6; break;
                    case 6: infoText.text = sObj.string7; break;
                    default: infoText.text = "エラーだよ";  break;
                }
                infoText.text = infoText.text.Replace("\\n", "\n");

            }
            else
            {
                infoText.text = textJaggedTbl[iconNo][level];
            }
        }
        else {
            Debug.Log("level text over!!" + level);
            infoText.text = "このテキストが出たらバグだよ";
        }


        // NEW、レベルを表示
        if (img.CheckIconNoIsUseItem(iconNo))
        {
            // ドロップアイテムは表示なし
            levelText.text = "";
        }
        else {
            // レベル文字設定
            if (level == 0)
            {
                levelText.text = "NEW!!";
                levelText.color = new Color(1, 0.5f, 0);
            }
            else
            {
                levelText.text = "LV." + (level + 1);
                levelText.color = Color.black;
            }
        }
    }

#if true
    // テキストテーブル
    string[][] textJaggedTbl = new string[][]{
        // 最大ＨＰアップ
        new string[]{
            "体力強化\n攻撃はライフで受ける！\n最大HPが２０％上がる",
            "体力強化\nさらに最大HPが２０％上がる",
            "体力強化\nさらに最大HPが２０％上がる",
            "体力強化\nさらに最大HPが２０％上がる",
            "体力強化\nさらに最大HPが２０％上がる",
        },
        // 自然回復アップ
        new string[]{
            "治癒力強化\n自然回復力をあげる！\n３秒毎にＨＰを１回復する",
            "治癒力強化\nさらに３秒毎にＨＰを１回復する",
            "治癒力強化\nさらに３秒毎にＨＰを１回復する",
            "治癒力強化\nさらに３秒毎にＨＰを１回復する",
            "治癒力強化\nさらに３秒毎にＨＰを１回復する",
        },
        // 防御力アップ
        new string[]{
            "防御力強化\n硬くなりたいあなたへ！\n被ダメージを３軽減する",
            "防御力強化\nさらに被ダメージを３軽減する",
            "防御力強化\nさらに被ダメージを３軽減する",
            "防御力強化\nさらに被ダメージを３軽減する",
            "防御力強化\nさらに被ダメージを３軽減する",
        },
        // 移動速度アップ
        new string[]{
            "移動速度強化\n素早さこそ正義！\n移動速度が５％上がる",
            "移動速度強化\nさらに移動速度が５％上がる",
            "移動速度強化\nさらに移動速度が５％上がる",
            "移動速度強化\nさらに移動速度が５％上がる",
            "移動速度強化\nさらに移動速度が５％上がる",
        },
        // 収集範囲アップ
        new string[]{
            "収集力強化\n驚きの吸引力を得る！\nアイテムの収集範囲が１００％上がる",
            "収集力強化\nさらにアイテムの収集範囲が１００％上がる",
            "収集力強化\nさらにアイテムの収集範囲が１００％上がる",
            "収集力強化\nさらにアイテムの収集範囲が１００％上がる",
            "収集力強化\nさらにアイテムの収集範囲が１００％上がる",
        },
        // 経験値アップ
        new string[]{
            "成長力強化\nレベル上げしよ！！\n経験値取得量が２０％上がる",
            "成長力強化\nさらに経験値取得量が２０％上がる",
            "成長力強化\nさらに経験値取得量が２０％上がる",
            "成長力強化\nさらに経験値取得量が２０％上がる",
            "成長力強化\nさらに経験値取得量が２０％上がる",
        },
        // 金アップ
        new string[]{
            "金運強化\n金策するならコレ！\nお金の取得量が２０％上がる",
            "金運強化\nさらにお金の取得量が２０％上がる",
            "金運強化\nさらにお金の取得量が２０％上がる",
            "金運強化\nさらにお金の取得量が２０％上がる",
            "金運強化\nさらにお金の取得量が２０％上がる",
        },
        // 運アップ
        new string[]{
            "運強化\n開運して良いものを拾えるかも！\n運が５０％上がる",
            "運強化\nさらに運が５０％上がる",
            "運強化\nさらに運が５０％上がる",
            "運強化\nさらに運が５０％上がる",
            "運強化\nさらに運が５０％上がる",
        },
        // 与ダメージアップ
        new string[]{
            "攻撃力強化\n火力が正義！\n与ダメージが１０％上がる",
            "攻撃力強化\nさらに与ダメージが１０％上がる",
            "攻撃力強化\nさらに与ダメージが１０％上がる",
            "攻撃力強化\nさらに与ダメージが１０％上がる",
            "攻撃力強化\nさらに与ダメージが１０％上がる",
        },
        // クリティカルアップ
        new string[]{
            "会心率強化\n会心の一撃！！\nクリティカル率を１０％上がる",
            "会心率強化\nさらにクリティカル率を１０％上がる",
            "会心率強化\nさらにクリティカル率を１０％上がる",
            "会心率強化\nさらにクリティカル率を１０％上がる",
            "会心率強化\nさらにクリティカル率を１０％上がる",
        },
        // 攻撃時間アップ
        new string[]{
            "攻撃時間強化\n攻撃時間がアップ！\n攻撃時間を１０％延長する※一部の武器のみ",
            "攻撃時間強化\n攻撃時間を１０％延長する\n※一部の武器のみ",
            "攻撃時間強化\n攻撃時間を１０％延長する\n※一部の武器のみ",
            "攻撃時間強化\n攻撃時間を１０％延長する\n※一部の武器のみ",
            "攻撃時間強化\n攻撃時間を１０％延長する\n※一部の武器のみ",
        },
        // 段数アップ
        new string[]{
            "弾数強化\n弾数こそが正義！\n対応した武器の発射数が増える",
            "弾数強化\n対応した武器の発射数が増える",
        },
        // 攻撃範囲アップ
        new string[]{
            "攻撃範囲強化\n攻撃を当てやすくなるのは実質火力！\n攻撃の範囲が１０％広くなる",
            "攻撃範囲強化\nさらに攻撃の範囲が１０％広くなる",
            "攻撃範囲強化\nさらに攻撃の範囲が１０％広くなる",
            "攻撃範囲強化\nさらに攻撃の範囲が１０％広くなる",
            "攻撃範囲強化\nさらに攻撃の範囲が１０％広くなる",
        },
        // 弾速アップ
        new string[]{
            "弾速強化\n弾速アップで当てやすくなる！\n攻撃の弾速が１０％早くなる",
            "弾速強化\nさらに攻撃の弾速が１０％早くなる",
            "弾速強化\nさらに攻撃の弾速が１０％早くなる",
            "弾速強化\nさらに攻撃の弾速が１０％早くなる",
            "弾速強化\nさらに攻撃の弾速が１０％早くなる",
        },
        // リキャストアップ
        new string[]{
            "攻撃間隔短縮\n手数が増えるのは実質火力！\n攻撃間隔を１０％短縮する",
            "攻撃間隔短縮\nさらに攻撃間隔を１０％短縮する",
            "攻撃間隔短縮\nさらに攻撃間隔を１０％短縮する",
            "攻撃間隔短縮\nさらに攻撃間隔を１０％短縮する",
            "攻撃間隔短縮\nさらに攻撃間隔を１０％短縮する",
        },

        // 機工(MACHINIST)
        new string[]{
            "ハンドガン\nハンドガン３連射！\n向いている方向に射撃を撃つ",
            "ハンドガン\n発射数が２発になる",
            "ハンドガン\n発射数が３発になる",
            "ハンドガン\n攻撃力が５０％上がる",
            "ハンドガン\n弾が敵を１回だけ貫通するようになる",
            "ハンドガン\nクリティカル率が２５％上がる",
            "ハンドガン\n攻撃間隔を５０％短縮する",
        },
        new string[]{
            "ドリル\nドリルは飛ばすもの！\n一番近くの敵にドリルを放つ",
            "ドリル\n攻撃力が５０％上がる",
            "ドリル\nクリティカル率が２５％上がる",
            "ドリル\n攻撃範囲が５０％上がる",
            "ドリル\n攻撃間隔を２５％短縮する",
            "ドリル\nさらに攻撃力が５０％上がる",
            "ドリル\nさらにクリティカル率が５０％上がる",
        },
        new string[]{
            "回転のこぎり\nのこぎり持って回転する！\nのこぎりを回転させ周囲を薙ぎ払う",
            "回転のこぎり\n攻撃範囲が２５％上がる",
            "回転のこぎり\n攻撃間隔を５０％短縮する",
            "回転のこぎり\n攻撃力が５０％上がる",
            "回転のこぎり\n回転速度が５０％上がる",
            "回転のこぎり\nさらに攻撃範囲が５０％上がる",
            "回転のこぎり\nのこぎりが２枚になる",
        },
        new string[]{
            "火炎放射\n焼き払え！！\n向いている方向を火炎放射で薙ぎ払う",
            "火炎放射\n発射数が増える",
            "火炎放射\n攻撃範囲が５０％上がる",
            "火炎放射\n弾速が５０％上がる",
            "火炎放射\n攻撃力が５０％上がる",
            "火炎放射\n攻撃間隔を５０％短縮する",
            "火炎放射\nさらに毒放射も行う",
        },
        new string[]{
            "タレット\nランダム発射なタレット！\nタレットを設置してランダムに弾を撃つ",
            "タレット\nタレットの弾の弾速が５０％上がる",
            "タレット\nタレットの攻撃時間が５０％上がる",
            "タレット\n攻撃力が５０％上がる",
            "タレット\n攻撃範囲が５０％上がる",
            "タレット\n攻撃間隔を２５％短縮する",
            "タレット\nタレットを二つ設置する",
        },
        // とらぶいオリジナル
        new string[]{
            "検知式波動砲\n波動砲、放て！！\n検知した敵に波動砲が降り注ぐ",
            "検知式波動砲\n攻撃範囲が５０％上がる",
            "検知式波動砲\n攻撃力が５０％上がる",
            "検知式波動砲\nビームの攻撃時間が５０％上がる",
            "検知式波動砲\n攻撃間隔を２５％短縮する",
            "検知式波動砲\nクリティカル率が５０％上がる",
            "検知式波動砲\n検知できる敵の数を増やす",
        },

        // 金
        new string[]{
            "金\n金300！金策だー！！",
        },
        // 回復アイテム
        new string[]{
            "ポーション\n回復と言ったらこれ！\nHPを20回復する",
        },
    };

#else
    // テキストテーブル
    string[][] textJaggedTbl = new string[][]{
        // 最大ＨＰアップ
        new string[]{
            "活力のマテリア\n攻撃をライフで受けられる！\n最大HPが２０％上がる",
            "活力のマテリア\nさらに最大HPが２０％上がる",
            "活力のマテリア\nさらに最大HPが２０％上がる",
            "活力のマテリア\nさらに最大HPが２０％上がる",
            "活力のマテリア\nさらに最大HPが２０％上がる",
        },
        // 自然回復アップ
        new string[]{
            "信仰のマテリア\n自然回復力をあげる！※ＭＰではない\n３秒毎にＨＰを１回復する",
            "信仰のマテリア\nさらに３秒毎にＨＰを１回復する",
            "信仰のマテリア\nさらに３秒毎にＨＰを１回復する",
            "信仰のマテリア\nさらに３秒毎にＨＰを１回復する",
            "信仰のマテリア\nさらに３秒毎にＨＰを１回復する",
        },
        // 防御力アップ
        new string[]{
            "剛柔のマテリア\nメインタンク来た！これで勝つる！\n被ダメージを３軽減する",
            "剛柔のマテリア\nさらに被ダメージを３軽減する",
            "剛柔のマテリア\nさらに被ダメージを３軽減する",
            "剛柔のマテリア\nさらに被ダメージを３軽減する",
            "剛柔のマテリア\nさらに被ダメージを３軽減する",
        },
        // 移動速度アップ
        new string[]{
            "戦技のマテリア\nすば〜やい〜\n移動速度が５％上がる",
            "戦技のマテリア\nさらに移動速度が５％上がる",
            "戦技のマテリア\nさらに移動速度が５％上がる",
            "戦技のマテリア\nさらに移動速度が５％上がる",
            "戦技のマテリア\nさらに移動速度が５％上がる",
        },
        // 収集範囲アップ
        new string[]{
            "すいとるマテリア\n驚きの吸引力\nアイテムの収集範囲が１００％上がる",
            "すいとるマテリア\nさらにアイテムの収集範囲が１００％上がる",
            "すいとるマテリア\nさらにアイテムの収集範囲が１００％上がる",
            "すいとるマテリア\nさらにアイテムの収集範囲が１００％上がる",
            "すいとるマテリア\nさらにアイテムの収集範囲が１００％上がる",
        },
        // 経験値アップ
        new string[]{
            "経験値アップマテリア\nレベル上げしよ！！\n経験値取得料が２０％上がる",
            "経験値アップマテリア\nさらに経験値取得料が２０％上がる",
            "経験値アップマテリア\nさらに経験値取得料が２０％上がる",
            "経験値アップマテリア\nさらに経験値取得料が２０％上がる",
            "経験値アップマテリア\nさらに経験値取得料が２０％上がる",
        },
        // 金アップ
        new string[]{
            "ギルアップマテリア\n金策するならコレ！\nギル取得料が２０％上がる",
            "ギルアップマテリア\nさらにギル取得料が２０％上がる",
            "ギルアップマテリア\nさらにギル取得料が２０％上がる",
            "ギルアップマテリア\nさらにギル取得料が２０％上がる",
            "ギルアップマテリア\nさらにギル取得料が２０％上がる",
        },
        // 運アップ
        new string[]{
            "ラッキーマテリア\n開運して良いものを拾えるかも！\n運が５０％上がる",
            "ラッキーマテリア\nさらに運が５０％上がる",
            "ラッキーマテリア\nさらに運が５０％上がる",
            "ラッキーマテリア\nさらに運が５０％上がる",
            "ラッキーマテリア\nさらに運が５０％上がる",
        },
        // 与ダメージアップ
        new string[]{
            "雄略のマテリア\nデュナミスの力！\n与ダメージが１０％上がる",
            "雄略のマテリア\nさらに与ダメージが１０％上がる",
            "雄略のマテリア\nさらに与ダメージが１０％上がる",
            "雄略のマテリア\nさらに与ダメージが１０％上がる",
            "雄略のマテリア\nさらに与ダメージが１０％上がる",
        },
        // クリティカルアップ
        new string[]{
            "武略のマテリア\n困ったらこれ！！\nクリティカル率を５％上がる",
            "武略のマテリア\nさらにクリティカル率を５％上がる",
            "武略のマテリア\nさらにクリティカル率を５％上がる",
            "武略のマテリア\nさらにクリティカル率を５％上がる",
            "武略のマテリア\nさらにクリティカル率を５％上がる",
        },
        // 攻撃時間アップ
        new string[]{
            "ためるマテリア\n力をためて攻撃時間がアップ！\n攻撃時間を１０％延長する※一部の武器のみ",
            "ためるマテリア\n攻撃時間を１０％延長する\n※一部の武器のみ",
            "ためるマテリア\n攻撃時間を１０％延長する\n※一部の武器のみ",
            "ためるマテリア\n攻撃時間を１０％延長する\n※一部の武器のみ",
            "ためるマテリア\n攻撃時間を１０％延長する\n※一部の武器のみ",
        },
        // 段数アップ
        new string[]{
            "ダブルマテリア\nダブル！って叫びたくなるやつ\n対応した武器の発射数が増える",
            "ダブルマテリア\n対応した武器の発射数が増える",
        },
        // 攻撃範囲アップ
        new string[]{
            "全体化マテリア\n攻撃を当てやすくなるのは実質火力！\n攻撃の範囲が１０％広くなる",
            "全体化マテリア\nさらに攻撃の範囲が１０％広くなる",
            "全体化マテリア\nさらに攻撃の範囲が１０％広くなる",
            "全体化マテリア\nさらに攻撃の範囲が１０％広くなる",
            "全体化マテリア\nさらに攻撃の範囲が１０％広くなる",
        },
        // 弾速アップ
        new string[]{
            "スピードマテリア\n弾速アップで当てやすくなる！\n攻撃の弾速が１０％早くなる",
            "スピードマテリア\nさらに攻撃の弾速が１０％早くなる",
            "スピードマテリア\nさらに攻撃の弾速が１０％早くなる",
            "スピードマテリア\nさらに攻撃の弾速が１０％早くなる",
            "スピードマテリア\nさらに攻撃の弾速が１０％早くなる",
        },
        // リキャストアップ
        new string[]{
            "詠唱のマテリア\n攻撃感覚が短くなるのは実質火力！\nリキャスト時間を１０％短縮する",
            "詠唱のマテリア\nリキャスト時間を１０％短縮する",
            "詠唱のマテリア\nリキャスト時間を１０％短縮する",
            "詠唱のマテリア\nリキャスト時間を１０％短縮する",
            "詠唱のマテリア\nリキャスト時間を１０％短縮する",
        },

        // 機工(MACHINIST)
        new string[]{
            "ショットコンボ\n機工士基本コンボ！\n向いている方向に射撃を撃つ",
            "ショットコンボ\n発射数が２発になる",
            "ショットコンボ\n発射数が３発になる",
            "ショットコンボ\n攻撃力が５０％上がる",
            "ショットコンボ\n弾が敵を１回だけ貫通するようになる",
            "ショットコンボ\nクリティカル率が２５％上がる",
            "ショットコンボ\nリキャスト時間を５０％短縮する",
        },
        new string[]{
            "ドリル\nドリドリドリ〜\n一番近くの敵にドリルを放つ",
            "ドリル\n攻撃力が５０％上がる",
            "ドリル\nクリティカル率が２５％上がる",
            "ドリル\n攻撃範囲が５０％上がる",
            "ドリル\nリキャスト時間を２５％短縮する",
            "ドリル\nさらに攻撃力が５０％上がる",
            "ドリル\nさらにクリティカル率が２５％上がる",
        },
        new string[]{
            "回転のこぎり\nのこぎり持ったら回転するよね\nのこぎりを回転させ周囲を薙ぎ払う",
            "回転のこぎり\n攻撃範囲が２５％上がる",
            "回転のこぎり\nリキャスト時間を５０％短縮する",
            "回転のこぎり\n攻撃力が５０％上がる",
            "回転のこぎり\n回転速度が２５％上がる",
            "回転のこぎり\nさらに攻撃範囲が２５％上がる",
            "回転のこぎり\nのこぎりが２枚になる",
        },
        new string[]{
            "火炎放射\n汚物は消毒よ〜！\n向いている方向を火炎放射で薙ぎ払う",
            "火炎放射\n発射数が増える",
            "火炎放射\n攻撃範囲が２５％上がる",
            "火炎放射\n弾速が５０％上がる",
            "火炎放射\n攻撃力が５０％上がる",
            "火炎放射\nリキャスト時間を５０％短縮する",
            "火炎放射\nさらに毒放射も行う",
        },
        new string[]{
            "タレット\n狙ったところは撃ってくれないタレット君\nタレットを設置してランダムに弾を撃つ",
            "タレット\nタレットの弾の弾速が５０％上がる",
            "タレット\nタレットの攻撃時間が５０％上がる",
            "タレット\n攻撃力が５０％上がる",
            "タレット\n攻撃範囲が５０％上がる",
            "タレット\nリキャスト時間を２５％短縮する",
            "タレット\nタレットを二つ設置する",
        },
        // とらぶいオリジナル
        new string[]{
            "検知式ぴま波動砲\n右ですか？左ですか？左右分かりません！\n検知した敵に波動砲が降り注ぐ",
            "検知式ぴま波動砲\n攻撃範囲が５０％上がる",
            "検知式ぴま波動砲\n攻撃力が５０％上がる",
            "検知式ぴま波動砲\nビームの攻撃時間が５０％上がる",
            "検知式ぴま波動砲\nリキャスト時間を２５％短縮する",
            "検知式ぴま波動砲\nクリティカル率が２５％上がる",
            "検知式ぴま波動砲\n検知できる敵の数を増やす",
        },

        // 金
        new string[]{
            "300ギル\n金策だー！！",
        },
        // 回復アイテム
        new string[]{
            "ポーション\n回復と言ったらこれ！\nHPを20回復する",
        },
    };
#endif
}
