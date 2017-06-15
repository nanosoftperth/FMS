 function numinwrd(value) {
    var numbr = value; var str = new String(numbr)
    var splt=str.split("");var rev=splt.reverse();var once=['Zero', ' One', 'Two', 'Three', 'Four',
    'Five', 'Six', 'Seven', 'Eight', 'Nine'];var twos=['Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen', ' Nineteen'];var tens=[ '', 'Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety' ];numlen=rev.length;var word=new Array();var j=0;
    for(i=0;i<numlen;i++){switch(i){case 0:if((rev[i]==0) || (rev[i+1]==1)){word[j]='';
    }else{word[j]=once[rev[i]];}word[j]=word[j] ;break;case 1:abovetens();
        break;case 2:if(rev[i]==0){word[j]='';} else if((rev[i-1]==0) || (rev[i-2]==0) ){word[j]=once[rev[i]]+"Hundred ";
        }else {word[j]=once[rev[i]]+"Hundred and";} break;case 3:if(rev[i]==0 || rev[i+1]==1){word[j]='';
        } else{word[j]=once[rev[i]];}if((rev[i+1]!=0) || (rev[i] > 0)){word[j]= word[j]+" Thousand";}break;
        case 4:abovetens(); break;
        case 5:if((rev[i]==0) || (rev[i+1]==1)){word[j]='';
        } else{word[j]=once[rev[i]];}word[j]=word[j]+"Lakhs";break;
        case 6:abovetens(); break;case 7:if((rev[i]==0) || (rev[i+1]==1)){word[j]='';
        } else{word[j]=once[rev[i]];}word[j]= word[j]+"Crore";break;
        case 8:abovetens(); break;
        default:break;}j++;
    } function abovetens()
    {if(rev[i]==0){word[j]='';} else if(rev[i]==1){word[j]=twos[rev[i-1]];}else{word[j]=tens[rev[i]];}
    }
    word.reverse();
    var finalw='';
    for(i=0;i<numlen;i++)
    {finalw= finalw+word[i];
    }
    return finalw;
}
 
 