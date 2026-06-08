// This file contains all the Common Java Script functions and veriables.

function DisplayErrorMessage(message) {
    alert(message);
}

function ValidateFloat(e, intDecimalPlaces) {
    var whichASC = (NS4) ? e.which : event.keyCode;
    var keyBS = 8;
    var keyDecimal = 46;
    var controlName = "";

    if (intDecimalPlaces == 0) {
        if (whichASC > 47 && whichASC < 58) {
            return true;
        }
        else {
            return false;
        }
    }


    if (NS4) {
        controlName = e.target.name;
        controlValue = e.target.value;
    }
    else {
        controlName = window.event.srcElement.getAttribute("name");
        controlValue = window.event.srcElement.getAttribute("value");
    }

    if ((((whichASC < 48) || (whichASC > 57)) && (!(whichASC == keyBS))) && (!(whichASC == keyDecimal))) {
        return (false);
    }
    else {
        if (whichASC == keyDecimal) {
            if (controlValue.indexOf(".") >= 0) {
                return false;
            }
            else
                return true;
        }
    }

    if (intDecimalPlaces != 0) {
        var el = window.event.srcElement;
        var cur_pos = IN4RE_CursorPosison(el); 	// return the cursor position
        //	
        if (cur_pos > (controlValue.indexOf(".") + 1)) {
            var txt = '';
            var foundIn = '';
            txt = document.selection.createRange().text;
            if (controlValue.indexOf(".") >= 0) {
                if ((controlValue.length - controlValue.indexOf(".")) > intDecimalPlaces)
                    if (txt == "") return false;
            }
        }
    }
    return (true);
}

var IN4SubmitId = "";
var IN4CloseId = "";
var IN4ChangeStatusId = "";
var IN4StatusDDLId = "";
var IN4RemarksId = "";
var IN4UpdateStatusId = "";
var IN4ScreenMode = 0;
var IN4Status = 0;



function GetElement(elemId)
{
    return document.getElementById(elemId);
}

function GetParentElement(elemId)
{
    return window.opener.getElementById(elemId);
}

function IN4_SetDisplay(elemID, isDisplay)
{
    var ElemIns = GetElement(elemID);
    if(ElemIns != null)
    {
        if(isDisplay == true)
        {
            ElemIns.style.display = '';
        }
        else
        {
            ElemIns.style.display = 'none';
        }
    }
}

function IN4_SetVisible(elemID, isVisible)
{
    var ElemIns = GetElement(elemID);
    if(ElemIns != null)
    {
        if(isVisible == true)
        {
            ElemIns.style.visibility = '';
        }
        else
        {
            ElemIns.style.visibility = 'none';
        }
    }
}

function IN4_SetEnable(elemID, isEnable)
{   
    var isReadOnly = !isEnable;
    var CurrentElem = GetElement(elemID);
    if(null == CurrentElem)
    {
        return;
    }
    var TagName = CurrentElem.tagName.toLowerCase();
    switch(TagName)
    {
        case "input":
            {
            switch(CurrentElem.type.toLowerCase())
            {
                case "text":
                case "password":
                    {
                        CurrentElem.readOnly = isReadOnly;
                        IN4_SetTabIndex(CurrentElem, isReadOnly);
                        break;
                    }
                case "button":
                case "radio":
                case "checkbox":
                case "submit":
                    {
                        IN4_SetTabIndexAndDisable(CurrentElem, isReadOnly);
                        break;
                    }
                case "hidden":
                    {  
                        break;
                    }
            }
            break;
            }
         case "textarea":
                    {
                        IN4_SetTabIndexAndDisable(CurrentElem, isReadOnly);
                        break;
                    }    
        case "select":
        case "button":
            {
                IN4_SetTabIndexAndDisable(CurrentElem, isReadOnly);
                break;
            }
            
        case "link":
            {
                if(CurrentElem.type.toLowerCase() == "text/css")
                {
                    return;
                }
                IN4_SetOrRemoveHREFAttribue(CurrentElem, isReadOnly);
                IN4_SetTabIndexAndDisable(CurrentElem, isReadOnly);
                
                break;
            }
        case "a":
        case "img":
            {
                IN4_SetTabIndexAndDisable(CurrentElem, isReadOnly);
                IN4_SetOrRemoveHREFAttribue(CurrentElem, isReadOnly);
                break;
            }
    }
}

function IN4_SetReadOnlyToGrid(GridIns, isReadOnly)
{
    
}

function IN4_SetTabIndexAndDisable(CurrentElem, isReadOnly)
{
    CurrentElem.disabled = isReadOnly;
    var TabIndex = null;
    if(true == isReadOnly)
    {
        TabIndex = -1;
    }

    CurrentElem.tabIndex = TabIndex;
}

function IN4_SetTabIndex(CurrentElem, isReadOnly)
{   
    var TabIndex = null;
    if(true == isReadOnly)
    {   
        TabIndex = -1;
    }

    //CurrentElem.style.backgroundColor = ElemColor;
    CurrentElem.tabIndex = TabIndex;
}

function IN4_SetOrRemoveHREFAttribue(lnkElem, isReadOnly)
{
    if(true == isReadOnly)
    {
        lnkElem.setAttribute("_href", lnkElem.getAttribute("href"));
        lnkElem.setAttribute("_onclick", lnkElem.getAttribute("onclick"));
        lnkElem.onclick = null;
        lnkElem.removeAttribute("href");
    }
    else
    {
        var _Href = lnkElem.getAttribute("_href");
        if(_Href != null)
        {
            lnkElem.setAttribute("href", _Href);
        }
        var _OnClick = lnkElem.getAttribute("_onclick");
        if(_OnClick != null)
        {
            lnkElem.setAttribute("onclick", _OnClick);
        }
        
        lnkElem.removeAttribute("_onclick");
        lnkElem.removeAttribute("_href");
    }
}



function IN4_ReplaceAll(sourceToReplace, fromString, toString)
{
    if(sourceToReplace == null || sourceToReplace == "" || fromString == null || fromString == "" || toString == null || toString == "")
    {
        return "";
    }
    sourceToReplace = sourceToReplace.toString();
    fromString = fromString.toString();
    toString = toString.toString();
    var Strs = sourceToReplace.split(fromString);
    var StrsCount = Strs.length - 1;
    var ReturnString = "";
    var NthPart = 0;
    for(NthPart = 0; NthPart < StrsCount; NthPart++)
    {
         ReturnString = ReturnString + Strs[NthPart] + toString;
    }
    return ReturnString + Strs[NthPart];
}


function IN4_DisplayWarningMessage(message)
{
    return confirm(message);
}

function IN4RE_EnableAllOnSubmit()
{
    IN4RE_EnableAllByTagName('SELECT');
    IN4RE_EnableAllByTagName('TEXTAREA');
    IN4RE_EnableAllByTagName('INPUT');
}

function IN4RE_EnableAllByTagName(tagName)
{
    var AllControls = document.getElementsByTagName(tagName);
    var AllControlsLenght = AllControls.length;
    for(var NthControl = 0; NthControl < AllControlsLenght; NthControl++)
    {
        AllControls[NthControl].disabled = false;
    }
    
}

function IN4RE_CloseWindow()
{
    window.opener = null;
    window.close();
    return false;
}

function IN4RE_Redirect(url)
{
    window.location.href = url;
     return false;
}


function IN4RE_GetOpener()
{
    if(window.opener != null)
    {
        return window.opener;
    }
    return window;
}

function IN4RE_OpenWindow(url, windowName, windowSize)
{
    if(windowSize == 0)
    {
        return window.open(url, windowName, 'status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
    }
    else if(windowSize == 1)
    {
        return window.open(url, windowName, 'width=450, height=260, left=200,top=200,screenX=200,screenY=200,status=no, resizable=yes, scrollbars=Yes, toolbar=no,location=no, menubar=no');
    }
    else if(windowSize == 2)
    {
        return window.open(url, windowName, 'width=900, height=520,status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no,left=50,right=50,top=130');
    }
    else if (windowSize == 3) 
    {
        return window.open(url, windowName, 'width=1050, height=700,status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no,left=0,top=0');
    }
    else if (windowSize == 4) //For Pop Up List Consultant/Contractor
    {
        return window.open(url, windowName, "menu=no,scrollbars=yes,width=270,height=350,resizable=no,top=300,left=600");
    }
}

function IN4RE_Click(elemID)
{    
    var ElemIns = GetElement(elemID);
    if(ElemIns != null)
    {
        ElemIns.click();
    }
}

function IN4RE_CheckBoxClick(elemID)
{    
    var ElemIns = GetElement(elemID);
    if(ElemIns != null)
    {   
        eval((ElemIns.onclick.toString().split('{')[1].split('}')[0]).replace("this", "GetElement(elemID)"));
    }
}

function IN4RE_SetFocus(elemID)
{  
    var ElemIns = GetElement(elemID);
    if(null == ElemIns || ElemIns.style.visibility == "hidden" || ElemIns.style.display == "none" || true == ElemIns.disabled || ElemIns.readOnly ==true)
    {
        return;
    } 
   try
   {
	ElemIns.focus();
   }
   catch(e){}
}


function IN4_RoundNumber(numberIns, decimalPlaces)
{  
    var NewNumber = ''; 
    numberIns = numberIns.toString(); //We need to operate on and return a string, not a numberIns
    decimalPlaces = decimalPlaces * 1; //We need an integer
    
    var CurrentPossitionOfDot = numberIns.lastIndexOf(".");
    //If there is nothing before the decimal point, prefix with a zero
    if (CurrentPossitionOfDot == 0)
    {
        numberIns = "0" + numberIns;
        CurrentPossitionOfDot = 1;
    }
    
    //Has an integer been passed in?
    if (CurrentPossitionOfDot == -1 || CurrentPossitionOfDot == numberIns.length - 1)
    {
        if (decimalPlaces > 0)
        {
            NewNumber = numberIns + ".";
            for(var NthDigit = 0; NthDigit < decimalPlaces; NthDigit++)
            {
                NewNumber += "0";
            }
            return (new Number(NewNumber));
        }
        else
        {
            return (new Number(numberIns));
        }
    }
    
    //Do we already have the right numberIns of decimal places?
    var CurrentNumberOfDecimales = (numberIns.length - 1) - CurrentPossitionOfDot;
    if (CurrentNumberOfDecimales == decimalPlaces)
    {
        return (new Number(numberIns)); //If so, just return the input value
    }
    
    //Do we already have less than the numberIns of decimal places we want?
    if (CurrentNumberOfDecimales < decimalPlaces)
    {
        //If so, pad out with zeros
        NewNumber = numberIns;
        for(var NthDigit = CurrentNumberOfDecimales; NthDigit < decimalPlaces; NthDigit++)
        {
            NewNumber += "0";
        }
        return (new Number(NewNumber));
    }
    
    //Work out whether to round up or not
    var LastPossition = (CurrentPossitionOfDot * 1) + decimalPlaces;
    var NeedRoundOf = false; //Whether or not to round up (add 1 to) the next digit along
    if ((numberIns.charAt(LastPossition + 1) * 1) > 4)
    {
        NeedRoundOf = true;
    }

    //Record each digit in an array for easier manipulation
    var DigitsArray = new Array();
    for(var NthDigit = 0; NthDigit <= LastPossition; NthDigit++)
    {
        DigitsArray[NthDigit] = numberIns.charAt(NthDigit);
    }
    
    //Round up the last digit if required, and continue until no more 9's are found
    var DoContinue = false;
    var DigitsLength = DigitsArray.length;
    for(var NthDigit = DigitsLength; NthDigit > CurrentPossitionOfDot; NthDigit--)
    {
        if (DigitsArray[NthDigit] == ".")
        {
            DoContinue = true;
            continue;
        }
        if (NeedRoundOf)
        {
            DigitsArray[NthDigit]++;
            if (DigitsArray[NthDigit] < 10)
            {
                break;
            }
        }
        else
        {
            break;
        }
        if (DoContinue == true)
        {
            break;
        }
    }

    for (var NthDigit = 0; NthDigit <= LastPossition; NthDigit++)
    {
        if (DigitsArray[NthDigit] == "." || DigitsArray[NthDigit] < 10)
        {
            NewNumber += DigitsArray[NthDigit];
        }
        else
        {
            NewNumber += "0";
        }
    }
    
    if (decimalPlaces == 0)
    {
            NewNumber = NewNumber.replace(".", "");
    }
    return (new Number(NewNumber));
}

function IN4Grid_SortNumeric(Number1, Number2)
{

    Number1 = Number1.replace(/,/,'.');
    Number2 = Number2.replace(/,/,'.');
    Number1 = Number1.replace(/[^\d\-\.\/]/g,'');
    Number2 = Number2.replace(/[^\d\-\.\/]/g,'');
    if(Number1.indexOf('/')>=0) 
    {
        Number1 = eval(Number1);
    }
    if(Number2.indexOf('/')>=0)
    {
        Number2 = eval(Number2);
    }
    return Number1/1 - Number2/1;
}

function IN4Grid_SortString(String1, String2) 
{
  if ( String1.toUpperCase() < String2.toUpperCase() ) return -1;
  if ( String1.toUpperCase() > String2.toUpperCase() ) return 1;
  return 0;
}

function IN4Grid_SortDate(Date1, Date2)
{
  if ( (new Date(Date1)).getTime() < (new Date(Date2)).getTime() ) return -1;
  if ( (new Date(Date1)).getTime() > (new Date(Date2)).getTime() ) return 1;
  return 0;
    
}
function IN4Grid_Init(gridId)
{
    var GridIns = GetElement(gridId);
    var AllRowsIns = GridIns.children[0].children;
    var CellsIns = AllRowsIns[0].children;
    var CellsInsLength = CellsIns.length;
    for(var NthCell = 0; NthCell < CellsInsLength; NthCell++)
    {		        
        var SortLink = CellsIns[NthCell].children;
        if(SortLink.length > 0)
        {
            SortLink = SortLink[0];
            var OnClickEvent = SortLink.getAttribute("href");
            if(OnClickEvent != null && OnClickEvent != "")
            {		                
                SortLink.setAttribute("href", OnClickEvent.replace("__doPostBack", "IN4Grid_Sort").replace("')", "','" + NthCell + "')"));
            }
        }
    }
}

function IN4Grid_Sort(gridID, eventIns, columnIndex)
{
    var SortingColumnName = eventIns.replace("Sort$", "");
    var GridIns = GetElement(gridID);
    if(GridIns == null)
    {
        GridIns = GetElement(IN4_ReplaceAll(gridID, "$", "_"));
    }
    var __Order = GridIns.getAttribute("__Order");
    var SortSymbol = ""; 
    if(__Order == null || __Order == " DESC ")
    {
        __Order = " ASC ";
        SortSymbol = "<font class='compulsory'>▲<font>";
    }
    else 
    {
        __Order = " DESC ";
        SortSymbol = "<font class='compulsory'>▼<font>";
    }		

    var GridBody = GridIns.getElementsByTagName('TBODY')[0];
    var AllCellTextArray = new Array();
    var AllCellArray = new Array();
    var AllRows = GridBody.rows;
    var AllRowsLenght = AllRows.length;
    for(var NthRow = 1; NthRow < AllRowsLenght; NthRow++)
    {
        var CurrentCell = AllRows[NthRow].cells[columnIndex];
	    AllCellTextArray.push(CurrentCell.innerHTML + '');
	    AllCellArray.push(CurrentCell);
    }
    var _DataType = GridIns.children[0].children[0].children[columnIndex].getAttribute("_DataType"); 
    if(_DataType == "System.String")
    {
	    AllCellTextArray = AllCellTextArray.sort(IN4Grid_SortString);
    }
    else if(_DataType == "Date")
    {
        AllCellTextArray = AllCellTextArray.sort(IN4Grid_SortDate);
       
    }
     else
    {
        AllCellTextArray = AllCellTextArray.sort(IN4Grid_SortNumeric);
       
    }
    
    var AllCellTextArrayLength = AllCellTextArray.length;
    var AllCellArrayLength = AllCellArray.length;
    if(__Order == " DESC ")
    {
        
	    for(var NthCellText = AllCellTextArrayLength; NthCellText >= 0; NthCellText--)
	    {
		    for(var NthCell = 0; NthCell < AllCellArrayLength; NthCell++)
		    {
			    if(AllCellArray[NthCell].innerHTML == AllCellTextArray[NthCellText] && !AllCellArray[NthCell].getAttribute('allreadySorted'))
			    {
				    AllCellArray[NthCell].setAttribute('allreadySorted','1');	
				    GridBody.appendChild(AllCellArray[NthCell].parentNode);				
			    }				
		    }			
	    }
    }
    else
    {
        for(var NthCellText = 0; NthCellText < AllCellTextArrayLength;  NthCellText++)
	    {
		    for(var NthCell = 0; NthCell < AllCellArrayLength; NthCell++)
		    {
			    if(AllCellArray[NthCell].innerHTML == AllCellTextArray[NthCellText] && !AllCellArray[NthCell].getAttribute('allreadySorted'))
			    {
				    AllCellArray[NthCell].setAttribute('allreadySorted','1');	
				    GridBody.appendChild(AllCellArray[NthCell].parentNode);				
			    }				
		    }			
	    }
    }
    
    for(var NthCell = 0; NthCell < AllCellArrayLength; NthCell++)
    {
	    AllCellArray[NthCell].removeAttribute('allreadySorted');		
    }
    
    GridIns.setAttribute("__Order", __Order);
    var HeaderRow = GridIns.children[0].children[0];
    var _OldIndex = GridIns.getAttribute("_OldIndex");
    if(_OldIndex != null && _OldIndex != "")
    {
        HeaderRow.children[_OldIndex].innerHTML = HeaderRow.children[_OldIndex].innerHTML.split("<FONT")[0];
    }
    HeaderRow.children[columnIndex].innerHTML = HeaderRow.children[columnIndex].innerHTML + SortSymbol;
    GridIns.setAttribute("_OldIndex", columnIndex);    
}

function IN4_StringInArray(stringIns, arrayIns)
 {
   var ArrayLength = arrayIns.length;
   for (var NtElem = 0; NtElem < ArrayLength; NtElem++)
   {
     if (stringIns == arrayIns[NtElem]) {return true;}
   }
   return false;
 }

function IN4_SetPageInReadOnlyMode(excludedControlIds)
{   
    if(excludedControlIds == null)
    {
        excludedControlIds = "";
    }
    if(IN4ScreenMode == 2)
    {
        excludedControlIds += IN4ChangeStatusId + ",";
        excludedControlIds += IN4StatusDDLId + ",";
        excludedControlIds += IN4RemarksId + ",";
        excludedControlIds += IN4UpdateStatusId + ",";
        excludedControlIds += IN4SubmitId;
    }
    else if(IN4Status == 2) // Approved
    {
        
    }
    
        excludedControlIds += "," + IN4CloseId;
    
     var AllElems = document.forms[0].elements;
     var ExcludedControls = excludedControlIds.split(',');
     IN4_SetPageToReadOnly_Recursive(AllElems.childNodes, ExcludedControls);   
     IN4P_EnableCloseButton();
}

function IN4P_EnableCloseButton()
{
    if(IN4ScreenMode == 2)
    {
        IN4_SetEnable(IN4ChangeStatusId, true);
        IN4_SetEnable(IN4StatusDDLId, true);
        IN4_SetEnable(IN4RemarksId, true);
        IN4_SetEnable(IN4UpdateStatusId, true);
        IN4_SetEnable(IN4SubmitId, true);
    }
    
    IN4_SetEnable(IN4CloseId, true);
}


function IN4_SetPageToReadOnly_Recursive(AllNodes, excludedControlsID)
{
   for (var NthControl=0; NthControl < AllNodes.length; NthControl++)
   {
        var CurrentNode = AllNodes[NthControl];
        var CurrentNodeName = CurrentNode.nodeName.toLowerCase();
        
     if (CurrentNode.nodeType == 1 && "tbody" != CurrentNodeName && "tr" != CurrentNodeName && "span" != CurrentNodeName )
     {  
       var CurrentNodeId   = CurrentNode.getAttribute("id");
       if(false == IN4_StringInArray(CurrentNodeId, excludedControlsID))
       {      
           switch(CurrentNodeName)
           {
             case "select":
             case "textarea":
             case "button":
                 {
                    IN4_SetTabIndexAndDisable(CurrentNode, true);
                    break;
                 }
             case "input":
                 {
                    switch (CurrentNode.getAttribute("type").toLowerCase())
                       {
                         case "text":                           
                            IN4_SetTabIndex(CurrentNode, true);     
                            CurrentNode.readOnly = true;
                           break;
                        case "hidden":
                            {   
                                break;
                            }
                        case "submit":
	                    case "button":
	                    case "radio":
	                    case "checkbox":		
		                    {   
			                    IN4_SetOrRemoveHREFAttribue(CurrentNode, true);
			                    IN4_SetTabIndexAndDisable(CurrentNode, true);
			                    break;
		                    }
		            }		
                break;
               } 
              case "link":
              case "a":
              case "img":
    		    	{
    		    	    if(CurrentNode.type != undefined && CurrentNode.type.toLowerCase() == "text/css")
                        {
                            break;
                        }
    			        IN4_SetOrRemoveHREFAttribue(CurrentNode, true);
    				    IN4_SetTabIndexAndDisable(CurrentNode, true);
      				    break;
    	    		}
            }
      }
      else if("table" == CurrentNodeName)
      {
            // If it is grid Do somthing
      }  
     }
     if (CurrentNode.hasChildNodes)
     {
        IN4_SetPageToReadOnly_Recursive(CurrentNode.childNodes, excludedControlsID);
     }
   }
}


var IN4WindowIns;
var IN4ControlsArray;
var IN4Title = "";
function IN4RE_PrintHTML(title, controlsArray, format)
{     
    IN4ControlsArray = controlsArray;
    IN4Title = title;
    IN4WindowIns = IN4RE_OpenWindow('/IN4REASPX/UI/Common/PrintDetails.aspx?Format=' + format.toString(), 'PrintDetails', 0);
}

var	IE4 = (document.all);
var NS4 = (document.layers);


function IN4RE_CursorPosison(el)
{
	var i = el.value.length + 1;

	if (el.createTextRange)
	{
		var cursor = document.selection.createRange().duplicate();
		while (cursor.parentElement() == el
			&& cursor.move("character", 1) == 1)
		{
			--i; 
		}
	} 
	return i;
}

// MultiLineTextBoxValidator
function IN4_MLTBValidator(multiLineTextBoxIns) 
{   
    var ControlValue = multiLineTextBoxIns.value;
    var MaxLength = multiLineTextBoxIns.getAttribute("MaxLength");
    MaxLength = parseInt(MaxLength);
    var countString = MaxLength - ControlValue.length; 
    if (countString < 0) 
    {
        multiLineTextBoxIns.value = ControlValue.substring(0, MaxLength);
        return false;
    }
}

function IN4_Date_Validate(dateTextBoxIns, isKeyPress)
{
    var EventIns = event;
    if(isKeyPress == false)
    {
        dateTextBoxIns.value = "";    
    }
    else
    {
        EventIns.keyCode = 0;
    }
}

//ADDED BY SHASHI.

function ConfirmDelete(parameterValue,parameterName)
{

    var conf = confirm("Do you want to delete " + parameterName + " : " + parameterValue + "?");
    
    if (conf)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//Only numbers input.   //Characters are not allowed
function varifyNumber(e)
{
    if (event.keyCode < 44 || event.keyCode > 57 || event.keyCode==47 || event.keyCode==46)
    {
        alert("Characters are not allowed.");
        event.returnValue = false;
    }
}
//Only numbers input.   //Characters are not allowed
function varifyNumber(e)
{
    if (event.keyCode < 44 || event.keyCode > 57 || event.keyCode==47 || event.keyCode==46)
    {
        alert("Characters are not allowed.");
        event.returnValue = false;
    }
}
//Only numbers input.   //Characters are not allowed
function varifyNumberPositive(e)
{  
    if (event.keyCode < 44 || event.keyCode > 57 || event.keyCode==47 || event.keyCode==45 || event.keyCode==46)
    {
        alert("Characters are not allowed.");
        event.returnValue = false;
    }
}  
//Only Characters input.   //numbers & Special characters are not allowed
function varifychar(e)
{
    if ( (event.keyCode >= 33 && event.keyCode <= 64)||(event.keyCode >= 91 && event.keyCode <= 96)||(event.keyCode >= 123 && event.keyCode <= 126) )
	{
		alert("Numbers & Special characters are not allowed.");
		event.returnValue = false;
	}
}
 //Only Special input      // numbers are not allowed 
function OnlySpecChar(e)
{
    if (event.keyCode >= 48 && event.keyCode <= 57 )
    {
        alert("Numbers are not allowed.");
        event.returnValue = false;
    }
} 
//Only Characters & numbers  //Special characters not allowed.
function NoSpecChar(e)
{
    if ( (event.keyCode >= 33 && event.keyCode <= 47) || (event.keyCode >= 58 && event.keyCode <= 64) ||(event.keyCode >= 91 && event.keyCode <= 96) || (event.keyCode >= 123 && event.keyCode <= 126) )
    {
        alert("Special characters are not allowed.");
        event.returnValue = false;
    }
} 

// Added by Srikanth.R
// only numbers and : is allowed (ratio validation)
function verifyRatio(e)
{
    if (event.keyCode < 44 || (event.keyCode > 57 && event.keyCode < 58) || event.keyCode >58 || 
                    event.keyCode==47 || event.keyCode==46)
    {      
        event.returnValue = false;
    }
}


//added by purna
function IN4_LengthValidation(DisplayObject,ContentObject,NoOfChar)
{

    var DisObject=GetElement(DisplayObject);
    DisObject.style.display = "block";
    DisObject.style.ReadOnly=true;
    
	var textVal;
	var length;
	textVal = ContentObject.value;
	length = textVal.length;
	if(length > NoOfChar)
	{		
		DisObject.style.backgroundColor='#FF3333';
		DisObject.value = length + "/" + NoOfChar;
		alert("Maximum " + NoOfChar + " characters are allowed.");
		ContentObject.value = textVal.substring(0,NoOfChar);
		DisObject.style.backgroundColor='#F5F5F5';
		DisObject.value = length + "/" + NoOfChar;
	}
	else
	{
		DisObject.value = length + "/" + NoOfChar;
		DisObject.style.backgroundColor='#F5F5F5';
	}

}

function validateFloat(e,intDecimalPlaces)
	{
	    var whichASC = (NS4) ? e.which : event.keyCode;
	    var keyBS = 8;
		var keyDecimal = 46;
		var controlName = "";		
		
		if (intDecimalPlaces == 0)
		{
			if (whichASC > 47 && whichASC < 58){		
				return true;}
			else {
				return false;}
		}
		if (NS4)
		{
			controlName = e.target.name;
			controlValue = e.target.value;
		}
		else
		{
			controlName = window.event.srcElement.getAttribute("name");
			if(window.event.srcElement.getAttribute("value")==null)
			{
			    controlValue = "";
			}
			else
			{
			    controlValue = window.event.srcElement.getAttribute("value");
			}
		}

	    if( (((whichASC < 48) || (whichASC > 57))  && (!(whichASC == keyBS))) && (!(whichASC == keyDecimal)))
	    {
			return (false);
	    }
		else
		{
			if (whichASC == keyDecimal)
			{
				if (controlValue.indexOf(".") >= 0)
					return false;
				else
					return true;	
			}
		}

		if (intDecimalPlaces != 0)
		{
			var el = window.event.srcElement;
			var cur_pos = cursor_pos(el);		// return the cursor position
			//	
			if(cur_pos > (controlValue.indexOf(".") + 1))
			{		
				var txt = '';
				var foundIn = '';		
				txt = document.selection.createRange().text;					
				if (controlValue.indexOf(".") >= 0)
				{	
					if ((controlValue.length - controlValue.indexOf(".")) > intDecimalPlaces)
						if(txt=="")  return false;
				}
			}
		}		
	    return (true);
	}
	function cursor_pos(el)
	{
		var i = el.value.length + 1;

		if (el.createTextRange)
		{
			var cursor = document.selection.createRange().duplicate();
			while (cursor.parentElement() == el
				&& cursor.move("character", 1) == 1)
			{
				--i; 
			}
		} 
		return i;
	}

	function validateFloatN(e, intDecimalPlaces) {
	    var whichASC = (NS4) ? e.which : event.keyCode;
	    var keyBS = 8;
	    var keyDecimal = 46;
	    var controlName = "";
	    if (intDecimalPlaces == 0) {
	        if (whichASC > 47 && whichASC < 58) {
	            return true;
	        }
	        else {
	            return false;
	        }
	    }
	    if (NS4) {
	        controlName = e.target.name;
	        controlValue = e.target.value;
	    }
	    else {
	        controlName = window.event.srcElement.getAttribute("name");
	        controlValue = window.event.srcElement.getAttribute("value");
	    }

	    if ((((whichASC < 48) || (whichASC > 57)) && (!(whichASC == keyBS))) && (!(whichASC == keyDecimal)) && (whichASC != 45)) //45 is '-' char -- modified by Amresh
	    {
	        return (false);
	    }
	    else {
	        if (whichASC == keyDecimal) {
	            if (controlValue.indexOf(".") >= 0)
	                return false;
	            else
	                return true;
	        }
	    }

	    if (intDecimalPlaces != 0) {
	        var el = window.event.srcElement;
	        var cur_pos = cursor_pos(el); 	// return the cursor position
	        //	
	        if (cur_pos > (controlValue.indexOf(".") + 1)) {
	            var txt = '';
	            var foundIn = '';
	            txt = document.selection.createRange().text;
	            if (controlValue.indexOf(".") >= 0) {
	                if ((controlValue.length - controlValue.indexOf(".")) > intDecimalPlaces)
	                    if (txt == "") return false;
	            }
	        }
	    }
	    return (true);
	}	
