Imports OSIsoft.AF




Public Class BackgroundCalcs

   

    ''' <summary>
    ''' will get the next starttime from PI 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetStartTime() As Date
        Return Now.AddDays(-30)
    End Function

    Public Sub New()

    End Sub


    '    bool IsPointInPolygon(List<Loc> poly, Loc point)
    '{
    '    int i, j;
    '    bool c = false;
    '    for (i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
    '    {
    '        if ((((poly[i].Lt <= point.Lt) && (point.Lt < poly[j].Lt)) 
    '                || ((poly[j].Lt <= point.Lt) && (point.Lt < poly[i].Lt))) 
    '                && (point.Lg < (poly[j].Lg - poly[i].Lg) * (point.Lt - poly[i].Lt) 
    '                    / (poly[j].Lt - poly[i].Lt) + poly[i].Lg))

    '            c = !c;
    '        }
    '    }

    '    return c;
    '}


End Class



Public Class Loc

    Public Property lt As Double
    Public lg As Double

    Public Sub New(lt As Double, lg As Double)

        Me.lt = lt
        Me.lg = lg

    End Sub

End Class