Friend Class Ability

    Public Name As String
    Public Effectivenesses As List(Of AbilityEffectiveness)

    Public Shared Function LoadAbilities() As List(Of Ability)

        Dim abilityList As New List(Of Ability)

        AddAbilityWithOneEffect("Benefit of Fire", "Fire", 0, abilityList)
        AddAbilityWithOneEffect("Grace of Water", "Water", 0, abilityList)
        AddAbilityWithOneEffect("Force of Nature", "Nature", 0, abilityList)
        AddAbilityWithOneEffect("Air Cushion", "Earth", 0, abilityList)
        AddAbilityWithOneEffect("Metallurgy", "Steel", 0, abilityList)
        AddAbilityWithOneEffect("Smooth Sailing", "Wind", 0, abilityList)
        AddAbilityWithOneEffect("Electromagnetic", "Electric", 0, abilityList)
        AddAbilityWithOneEffect("Absorbent", "Light", 0, abilityList)
        AddAbilityWithOneEffect("Negative Aura", "Dark", 0, abilityList)
        AddAbilityWithOneEffect("Self Exorcism", "Nether", 0, abilityList)
        AddAbilityWithOneEffect("Appeased Spirit", "Nether", 0, abilityList)
        AddAbilityWithOneEffect("Strict Dosage", "Poison", 0, abilityList)
        AddAbilityWithOneEffect("Master's Defense", "Fighting", 0, abilityList)
        AddAbilityWithOneEffect("Unwavering Heart", "Illusion", 0, abilityList)
        AddAbilityWithOneEffect("Sound Absorb", "Sound", 0, abilityList)
        AddAbilityWithOneEffect("In Sync", "Warped", 0, abilityList)

        Dim cloakOfDarknessEffectivenesses As New List(Of AbilityEffectiveness)
        AddEffectiveness("Dark", 0, cloakOfDarknessEffectivenesses)
        AddEffectiveness("Light", 1.25, cloakOfDarknessEffectivenesses)
        AddAbilityWithMultipleEffects("Cloak of Darkness", cloakOfDarknessEffectivenesses, abilityList)

        Dim spiritOfYangEffectivenesses As New List(Of AbilityEffectiveness)
        AddEffectiveness("Electric", 0.8, spiritOfYangEffectivenesses)
        AddEffectiveness("Light", 0.8, spiritOfYangEffectivenesses)
        AddEffectiveness("Illusion", 0.8, spiritOfYangEffectivenesses)
        AddAbilityWithMultipleEffects("Spirit of Yang", spiritOfYangEffectivenesses, abilityList)

        Dim spiritOfYinEffectivenesses As New List(Of AbilityEffectiveness)
        AddEffectiveness("Dark", 0.8, spiritOfYinEffectivenesses)
        AddEffectiveness("Nether", 0.8, spiritOfYinEffectivenesses)
        AddEffectiveness("Poison", 0.8, spiritOfYinEffectivenesses)
        AddAbilityWithMultipleEffects("Spirit of Yin", spiritOfYinEffectivenesses, abilityList)

        Dim inverseReactionEffectivenesses As New List(Of AbilityEffectiveness)
        AddEffectiveness("Light", 0.5, inverseReactionEffectivenesses)
        AddEffectiveness("Dark", 0.5, inverseReactionEffectivenesses)
        AddAbilityWithMultipleEffects("Inverse Reaction", inverseReactionEffectivenesses, abilityList)

        Return abilityList

    End Function

    Private Shared Sub AddAbilityWithOneEffect(abilityName As String, typeName As String, effectiveness As Double, ByRef list As List(Of Ability))

        list.Add(New Ability With {
            .Name = abilityName,
            .Effectivenesses = New List(Of AbilityEffectiveness) From {
                New AbilityEffectiveness With {
                    .TypeName = typeName,
                    .Effectiveness = effectiveness
                }
            }
        })

    End Sub

    Private Shared Sub AddAbilityWithMultipleEffects(abilityName As String, effectivenessList As List(Of AbilityEffectiveness), ByRef list As List(Of Ability))

        list.Add(New Ability With {
            .Name = abilityName,
            .Effectivenesses = effectivenessList
        })

    End Sub

    Private Shared Sub AddEffectiveness(typeName As String, effectiveness As Double, ByRef list As List(Of AbilityEffectiveness))

        list.Add(New AbilityEffectiveness With {
            .TypeName = typeName,
            .Effectiveness = effectiveness
        })

    End Sub

End Class