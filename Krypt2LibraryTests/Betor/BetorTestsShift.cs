﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Text;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class BetorTestsShift
    {
        [DataRow("Output of Encrypt(♫♪) should not change if the code changes, because that will break backwards compatibility.",
            "♪♫WA♪:zvQ5CmV:*39p72T YW1HYnAOtINF3wf5v&.Q47yC##6-♫ta*Pl$G2JI)Vwcbew&4o**UuQHlGGsUiuWD-hX;H♫e(3!1y?3q8zGOoKLD-0")]
        [DataRow("Therefore God has highly exalted him and bestowed on him the name that is above every name, so that at the name of Jesus every knee should bow, in heaven and on earth and under the earth, and every tongue confess that Jesus Christ is Lord, to the glory of God the Father.",
            "2m?(ji$7yoX'%tQ-5R1:?$UZo*MMYLDo2Bg';;(.C0VCcau-;+lCukbDDZymTK-kh.*7I%Y1sRleHQQz+L0KR:.\"I9\"H:93Q5QvOhPtyEH(::+-ed3#-2@&mj)U7,K1Q*'IHH?Y\"?P1\"hl3o!;QfIy:$&\"zC*QXvq8'vYA?kyUYC@'c12ja;l@&3ESY;DDY1dcGDxXoY.0d'pMvgg:KtR?8Q#iz*Tj%0s-vd1-ojhoX#uo!ETc!8bV'Kp&1%4nhMqc)qUqO*9mzGJgr")]
        [TestMethod()]
        public void EncryptTestForConsistentOutputUsingShift(string message, string expectedCipherText)
        {
            var betor = new Betor(CharacterSwapMethod.Shift);
            var passphrase = "HereWeTest";

            var cipherText = betor.Encrypt(passphrase, message, null);

            Assert.AreEqual(expectedCipherText, cipherText);
        }

        
        [DataRow("If we encrypt text and then decrypt that cipherText, the decrypted text should exactly match the original text")]
        [DataRow("En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi.")]
        [DataRow("Welgeluksalig is die man wat nie wandel in die raad van die goddelose en nie staan op die weg van die sondaars en nie sit in die kring van die spotters nie; maar sy behae is in die wet van die HERE, en hy oordink sy wet dag en nag. En hy sal wees soos ’n boom wat geplant is by waterstrome, wat sy vrugte gee op sy tyd en waarvan die blare nie verwelk nie; en alles wat hy doen, voer hy voorspoedig uit. So is die goddelose mense nie, maar soos kaf wat die wind verstrooi. Daarom sal die goddelose nie bestaan in die oordeel en die sondaars in die vergadering van die regverdiges nie. Want die HERE ken die weg van die regverdiges, maar die weg van die goddelose sal vergaan.")]
        [DataRow("Κύριος Ἰησοῦς")]
        [DataRow("   ")]
        [DataRow("")]
        [DataRow("👩🏽‍🚒 Testing some Grapheme clusters. á  á")]  // Althoug this passes this test, it throws an exception if one tries to write cipherText to file.
        [DataRow("[1] PAULUS, ’n gevangene van Christus Jesus, en die broeder Timótheüs, aan Filémon, die geliefde en ons mede-arbeider, [2] en aan Áppia, die geliefde suster, en aan Archíppus, ons medestryder, en aan die gemeente wat in jou huis is: [3] Genade vir julle en vrede van God onse Vader en die Here Jesus Christus!")]
        [DataRow("Paul, an apostle of Christ Jesus by the will of God, and Timothy our brother, To the saints and faithful brothers in Christ at Colossae: Grace to you and peace from God our Father. We always thank God, the Father of our Lord Jesus Christ, when we pray for you, since we heard of your faith in Christ Jesus and of the love that you have for all the saints, because of the hope laid up for you in heaven. Of this you have heard before in the word of the truth, the gospel, which has come to you, as indeed in the whole world it is bearing fruit and increasing—as it also does among you, since the day you heard it and understood the grace of God in truth, just as you learned it from Epaphras our beloved fellow servant. He is a faithful minister of Christ on your behalf and has made known to us your love in the Spirit. And so, from the day we heard, we have not ceased to pray for you, asking that you may be filled with the knowledge of his will in all spiritual wisdom and understanding, so as to walk in a manner worthy of the Lord, fully pleasing to him: bearing fruit in every good work and increasing in the knowledge of God; being strengthened with all power, according to his glorious might, for all endurance and patience with joy; giving thanks to the Father, who has qualified you to share in the inheritance of the saints in light. He has delivered us from the domain of darkness and transferred us to the kingdom of his beloved Son, in whom we have redemption, the forgiveness of sins. He is the image of the invisible God, the firstborn of all creation. For by him all things were created, in heaven and on earth, visible and invisible, whether thrones or dominions or rulers or authorities—all things were created through him and for him. And he is before all things, and in him all things hold together. And he is the head of the body, the church. He is the beginning, the firstborn from the dead, that in everything he might be preeminent. For in him all the fullness of God was pleased to dwell, and through him to reconcile to himself all things, whether on earth or in heaven, making peace by the blood of his cross. And you, who once were alienated and hostile in mind, doing evil deeds, he has now reconciled in his body of flesh by his death, in order to present you holy and blameless and above reproach before him, if indeed you continue in the faith, stable and steadfast, not shifting from the hope of the gospel that you heard, which has been proclaimed in all creation under heaven, and of which I, Paul, became a minister. Now I rejoice in my sufferings for your sake, and in my flesh I am filling up what is lacking in Christ’s afflictions for the sake of his body, that is, the church, of which I became a minister according to the stewardship from God that was given to me for you, to make the word of God fully known, the mystery hidden for ages and generations but now revealed to his saints. To them God chose to make known how great among the Gentiles are the riches of the glory of this mystery, which is Christ in you, the hope of glory. Him we proclaim, warning everyone and teaching everyone with all wisdom, that we may present everyone mature in Christ. For this I toil, struggling with all his energy that he powerfully works within me. For I want you to know how great a struggle I have for you and for those at Laodicea and for all who have not seen me face to face, that their hearts may be encouraged, being knit together in love, to reach all the riches of full assurance of understanding and the knowledge of God’s mystery, which is Christ, in whom are hidden all the treasures of wisdom and knowledge. I say this in order that no one may delude you with plausible arguments. For though I am absent in body, yet I am with you in spirit, rejoicing to see your good order and the firmness of your faith in Christ. Therefore, as you received Christ Jesus the Lord, so walk in him, rooted and built up in him and established in the faith, just as you were taught, abounding in thanksgiving. See to it that no one takes you captive by philosophy and empty deceit, according to human tradition, according to the elemental spirits of the world, and not according to Christ. For in him the whole fullness of deity dwells bodily, and you have been filled in him, who is the head of all rule and authority. In him also you were circumcised with a circumcision made without hands, by putting off the body of the flesh, by the circumcision of Christ, having been buried with him in baptism, in which you were also raised with him through faith in the powerful working of God, who raised him from the dead. And you, who were dead in your trespasses and the uncircumcision of your flesh, God made alive together with him, having forgiven us all our trespasses, by canceling the record of debt that stood against us with its legal demands. This he set aside, nailing it to the cross. He disarmed the rulers and authorities and put them to open shame, by triumphing over them in him. Therefore let no one pass judgment on you in questions of food and drink, or with regard to a festival or a new moon or a Sabbath. These are a shadow of the things to come, but the substance belongs to Christ. Let no one disqualify you, insisting on asceticism and worship of angels, going on in detail about visions, puffed up without reason by his sensuous mind, and not holding fast to the Head, from whom the whole body, nourished and knit together through its joints and ligaments, grows with a growth that is from God. If with Christ you died to the elemental spirits of the world, why, as if you were still alive in the world, do you submit to regulations— “Do not handle, Do not taste, Do not touch” (referring to things that all perish as they are used)—according to human precepts and teachings? These have indeed an appearance of wisdom in promoting self-made religion and asceticism and severity to the body, but they are of no value in stopping the indulgence of the flesh. If then you have been raised with Christ, seek the things that are above, where Christ is, seated at the right hand of God. Set your minds on things that are above, not on things that are on earth. For you have died, and your life is hidden with Christ in God. When Christ who is your life appears, then you also will appear with him in glory. Put to death therefore what is earthly in you: sexual immorality, impurity, passion, evil desire, and covetousness, which is idolatry. On account of these the wrath of God is coming. In these you too once walked, when you were living in them. But now you must put them all away: anger, wrath, malice, slander, and obscene talk from your mouth. Do not lie to one another, seeing that you have put off the old self with its practices and have put on the new self, which is being renewed in knowledge after the image of its creator. Here there is not Greek and Jew, circumcised and uncircumcised, barbarian, Scythian, slave, free; but Christ is all, and in all. Put on then, as God’s chosen ones, holy and beloved, compassionate hearts, kindness, humility, meekness, and patience, bearing with one another and, if one has a complaint against another, forgiving each other; as the Lord has forgiven you, so you also must forgive. And above all these put on love, which binds everything together in perfect harmony. And let the peace of Christ rule in your hearts, to which indeed you were called in one body. And be thankful. Let the word of Christ dwell in you richly, teaching and admonishing one another in all wisdom, singing psalms and hymns and spiritual songs, with thankfulness in your hearts to God. And whatever you do, in word or deed, do everything in the name of the Lord Jesus, giving thanks to God the Father through him. Wives, submit to your husbands, as is fitting in the Lord. Husbands, love your wives, and do not be harsh with them. Children, obey your parents in everything, for this pleases the Lord. Fathers, do not provoke your children, lest they become discouraged. Bondservants, obey in everything those who are your earthly masters, not by way of eye-service, as people-pleasers, but with sincerity of heart, fearing the Lord. Whatever you do, work heartily, as for the Lord and not for men, knowing that from the Lord you will receive the inheritance as your reward. You are serving the Lord Christ. For the wrongdoer will be paid back for the wrong he has done, and there is no partiality. Masters, treat your bondservants justly and fairly, knowing that you also have a Master in heaven. Continue steadfastly in prayer, being watchful in it with thanksgiving. At the same time, pray also for us, that God may open to us a door for the word, to declare the mystery of Christ, on account of which I am in prison— that I may make it clear, which is how I ought to speak. Walk in wisdom toward outsiders, making the best use of the time. Let your speech always be gracious, seasoned with salt, so that you may know how you ought to answer each person. Tychicus will tell you all about my activities. He is a beloved brother and faithful minister and fellow servant in the Lord. I have sent him to you for this very purpose, that you may know how we are and that he may encourage your hearts, and with him Onesimus, our faithful and beloved brother, who is one of you. They will tell you of everything that has taken place here. Aristarchus my fellow prisoner greets you, and Mark the cousin of Barnabas (concerning whom you have received instructions—if he comes to you, welcome him), and Jesus who is called Justus. These are the only men of the circumcision among my fellow workers for the kingdom of God, and they have been a comfort to me. Epaphras, who is one of you, a servant of Christ Jesus, greets you, always struggling on your behalf in his prayers, that you may stand mature and fully assured in all the will of God. For I bear him witness that he has worked hard for you and for those in Laodicea and in Hierapolis. Luke the beloved physician greets you, as does Demas. Give my greetings to the brothers at Laodicea, and to Nympha and the church in her house. And when this letter has been read among you, have it also read in the church of the Laodiceans; and see that you also read the letter from Laodicea. And say to Archippus, “See that you fulfill the ministry that you have received in the Lord.” I, Paul, write this greeting with my own hand. Remember my chains. Grace be with you.")]
        [TestMethod()]
        public void EncryptDecryptUsingShiftTest(string message)
        {
            var betor = new Betor(CharacterSwapMethod.Shift);

            var passphrase = "HereWeAlwaysTestBecauseWeDistrustOurGuesses";

            var cipherText = betor.Encrypt(passphrase, message, null);

            var decryptedText = betor.Decrypt(passphrase, cipherText, null);

            Assert.AreEqual(message, decryptedText);
        }

        [DataRow("This string does not contain any grapheme clusters. It is a sanity check for the test itself. Κύριος Ἰησοῦς")]
        [DataRow("👩🏽‍🚒 Testing some Grapheme clusters. á  á")]
        [TestMethod()]
        public void UnicodeGraphemeClustersUsingShiftTest(string message)
        {
            var betor = new Betor(CharacterSwapMethod.Shift);

            var passphrase = "https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction";

            var cipherText = betor.Encrypt(passphrase, message, null);
            var decryptedText = betor.Decrypt(passphrase, cipherText, null);

            Assert.AreEqual(cipherText, Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(cipherText)));
            Assert.AreEqual(decryptedText, Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(decryptedText)));
        }

        [DataRow("This string does not contain any grapheme clusters. It is a sanity check for the test itself. Κύριος Ἰησοῦς")]
        [DataRow("👩🏽‍🚒 Testing some Grapheme clusters. á  á")]
        [TestMethod()]
        public void UnicodeGraphemeClustersUsingShuffleTest(string message)
        {
            var betor = new Betor(CharacterSwapMethod.Shuffle);

            var passphrase = "https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction";

            var cipherText = betor.Encrypt(passphrase, message, null);
            var decryptedText = betor.Decrypt(passphrase, cipherText, null);

            Assert.AreEqual(cipherText, Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(cipherText)));
            Assert.AreEqual(decryptedText, Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(decryptedText)));
        }


        [TestMethod()]
        public void InvalidCipherTextTest()
        {
            // ToDo: Let it test for throwing of exception...
            // Also: Reconsider how IndexOutOfrange is handled. Instead of handling that exception, perhaps check for index of [-1] and handle it ourselves instead of handling an exception.
            
            var betor = new Betor(CharacterSwapMethod.Shuffle);

            var passphrase = "InvalidCipherTextShouldNotTHrowAnException";

            var cipherText = "ghgh~+";

            Assert.ThrowsException<InvalidCipherException>(() => betor.Decrypt(passphrase, cipherText, null));

        }
    }
}