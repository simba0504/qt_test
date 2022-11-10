using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WASender.Models;

namespace WASender
{
    public class WAPIHelper
    {

        public static string ReradFile()
        {
            return System.IO.File.ReadAllText(@"wapi.js");
        }

        public static void injectWapi(IWebDriver driver)
        {
            try
            {
                IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
                jssend.ExecuteScript(ReradFile());
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {

            }
        }

        public static List<string> GetGroupMembers(WAPI_GroupModel groupModel, IWebDriver driver)
        {
            List<string> members = new List<string>();
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "var _lits=[]; var _members = await WAPI.getGroupParticipantIDs('" + groupModel.GroupId + "',true); for(var i=0;i< _members.length;i++) { _lits.push(_members[i].user); } return JSON.stringify(_lits);";
            string s = (string)jssend.ExecuteScript(exJS);
            members = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(s);
            return members;
        }
        public static bool IsWAPIInjected(IWebDriver driver)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string type = (string)jssend.ExecuteScript("return typeof(WAPI)");
            if (type == "object")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void sendTextMessage(IWebDriver driver, string number, string message)
        {
            string id = "";
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "await await WPP.chat.sendTextMessage('" + number + "@c.us',`" + message + "`);";
            jssend.ExecuteScript(exJS);
        }


        public static void OpenChat(IWebDriver driver,string number)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "WPP.chat.openChatFromUnread('" + number + "@c.us')";
            jssend.ExecuteScript(exJS);
        }

        public static void sendSeen(IWebDriver driver, string number)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "WAPI.sendSeen('" + number + "@c.us')";
            jssend.ExecuteScript(exJS);
        }
        public static void OpenMe(IWebDriver driver)
        {
            string id = "";
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "var meId=-1; for(var i=0;i< window.Store.Contact._models.length;i++) { if(window.Store.Contact._models[i].__x_isMe==true) { meId=i;}   } var  me=window.Store.Contact._models[meId]; await WPP.chat.openChatFromUnread(me.__x_id._serialized)";
            jssend.ExecuteScript(exJS);
        }
        public static bool sendButtonWithMessage(IWebDriver driver, MesageModel message, string toNumber, string FinalMessage)
        {

            string jsString = "";
            FinalMessage = FinalMessage.Replace("\n", "");
            jsString += "return await WAPI.sendButtons('" + toNumber + "@c.us', `" + FinalMessage + "`, [";

            string buttonString = "";
            foreach (var item in message.buttons)
            {
                if (buttonString != "")
                {
                    buttonString += ",";
                }
                buttonString += "{";
                buttonString += "id: '" + Guid.NewGuid() + "',";
                buttonString += "'text': '" + item.text + "',";
                if (item.buttonTypeEnum == enums.ButtonTypeEnum.PHONE_NUMBER)
                {
                    buttonString += "'phoneNumber': '" + item.phoneNumber + "',";
                }
                if (item.buttonTypeEnum == enums.ButtonTypeEnum.URL)
                {
                    buttonString += "'url': '" + item.url + "',";
                }
                buttonString += "}";
            }

            jsString += buttonString;
            jsString += "    ])";

            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            bool result = (bool)jssend.ExecuteScript(jsString);

            return result;
        }

        public static bool validateNumber(IWebDriver driver, string number)
        {
            try
            {
                IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
                string exJS = "var  ss= await WAPI.checkNumberStatus2('" + number + "@c.us'); return ss.numberExists;";
                bool s = (bool)jssend.ExecuteScript(exJS);
                return s;
            }
            catch (Exception ex)
            {
                string ss = "";
                return false;
            }
        }

        public static void SendImage(IWebDriver driver, string number, string ImageBase64, bool isGroup = false)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;

            string id = "";
            if (isGroup == true)
            {
                id = number + "@g.us";
            }
            else
            {
                id = number + "@c.us";
            }

            string exJS = "await WAPI.sendImage('" + ImageBase64 + "','" + id + "','ss','',true)";
            jssend.ExecuteScript(exJS);
        }
        public static void SendAttachment(IWebDriver driver, string number, string ImageBase64, string FileName)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "await WAPI.sendImage('" + ImageBase64 + "','" + number + "@c.us','" + FileName + "','',true)";
            jssend.ExecuteScript(exJS);
        }
        public static void SendAttachmentToGroup(IWebDriver driver, string GroupId, string ImageBase64, string FileName)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "await WAPI.sendImage('" + ImageBase64 + "','" + GroupId + "','" + FileName + "','',true)";
            jssend.ExecuteScript(exJS);
        }
        public static void sendConversationSeen(IWebDriver driver, string Number)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "WAPI.sendConversationSeen('" + Number + "@c.us')";
            jssend.ExecuteScript(exJS);
        }
        public static void SendMessage(IWebDriver driver, string number, string message, bool isGroup = false)
        {
            string id = "";
            if (isGroup == true)
            {
                id = number;
            }
            else
            {
                id = number + "@c.us";
            }

            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "await WAPI.sendMessageToID('" + id + "',`" + message + "`,true)";
            jssend.ExecuteScript(exJS);
        }


        public static List<string> GetAllChatIds(IWebDriver driver)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "return JSON.stringify(WAPI.getAllChatIds());";
            string s = (string)jssend.ExecuteScript(exJS);
            List<string> modelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(s);
            return modelList;
        }
        public static List<UnReadMessagesModel> _newMessagesBuffer(IWebDriver driver)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "for(var msgList=[],OriginalList=await WAPI._newMessagesBuffer,i=0;i<OriginalList.length;i++)!1==OriginalList[i].isGroupMsg&&OriginalList[0].from.user==OriginalList[0].chatId.user&& void 0!=OriginalList[i].body&&msgList.push({id:OriginalList[i].id,body:OriginalList[i].body,chatId:OriginalList[i].chatId.user}); return JSON.stringify(msgList);";
            string s = (string)jssend.ExecuteScript(exJS);
            List<UnReadMessagesModel> modelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UnReadMessagesModel>>(s);
            return modelList;
        }
        public static List<UnReadMessagesModel> GetAllUnreadMessages(IWebDriver driver)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "for(var msgList=[],OriginalList=await WAPI.getAllUnreadMessages(),i=0;i<OriginalList.length;i++)!1==OriginalList[i].isGroupMsg&&OriginalList[0].from.user==OriginalList[0].chatId.user&& void 0!=OriginalList[i].body&&msgList.push({id:OriginalList[i].id,body:OriginalList[i].body,chatId:OriginalList[i].chatId.user}); return JSON.stringify(msgList);";
            string s = (string)jssend.ExecuteScript(exJS);
            List<UnReadMessagesModel> modelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UnReadMessagesModel>>(s);
            return modelList;
        }
        public static bool GetGroup__x_canSend(IWebDriver driver, string GroupId)
        {
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            string exJS = "function checkid(x){return x.__x_id._serialized=='" + GroupId + "';}; return await WAPI.getAllGroups().filter(checkid)[0].__x_canSend ";
            return (bool)jssend.ExecuteScript(exJS);
        }



        public static List<WAPI_GroupModel> getMyGroups(IWebDriver driver)
        {
            List<WAPI_GroupModel> modelList = new List<WAPI_GroupModel>();
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            try
            {
                string exJS = @"var list=[];var allData=WAPI.getAllGroups();for(var i=0;i< allData.length;i++ ){list.push({'GroupId':allData[i].__x_id._serialized,'GroupName':allData[i].__x_formattedTitle});}return JSON.stringify(list) ;";
                exJS = exJS.Replace("\n", "");
                exJS = exJS.Replace("\r", "");
                exJS = exJS.Replace("\t", "");
                string s = (string)jssend.ExecuteScript(exJS);

                modelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WAPI_GroupModel>>(s);

            }
            catch (Exception ex)
            {

            }
            return modelList;
        }

        public static List<WAPI_ContactModel> getMyContacts(IWebDriver driver)
        {
            List<WAPI_ContactModel> WAPI_ContactModelList = new List<WAPI_ContactModel>();
            IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
            try
            {
                string exJS = @"var list=[];var allData=WAPI.getMyContacts();for(var i=0;i< allData.length;i++ ){list.push({'number':allData[i].id.user,'Name':allData[i].name});}  return JSON.stringify(list) ;";
                exJS = exJS.Replace("\n", "");
                exJS = exJS.Replace("\r", "");
                exJS = exJS.Replace("\t", "");
                string s = (string)jssend.ExecuteScript(exJS);

                WAPI_ContactModelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WAPI_ContactModel>>(s);

            }
            catch (Exception ex)
            {

            }
            return WAPI_ContactModelList;
        }


    }
}
