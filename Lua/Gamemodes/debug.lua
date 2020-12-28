--Fight to the Death: The Gamemode


function InitializeGamemode()
	return {"Debug",10} --Initialize a Gamemode with the name "Lua Test" and the ID of 6. In the future, the ID will be determined by the server/loader.
end

function OnTaskCompletionClient(totaltasks,completedtasks,player)
	print("how") --how
	return false
end

function OnTaskCompletionHost(totaltasks,completedtasks,player)
	return false --dumb
end

function CheckWinCondition(impostors,crewmates,sab,taskscomplete) --required
	if (sab) then
		return "impostors"
	end
	return "none"
end

function DecideRoles(playerinfos)
	return {{},{}} -- no roles, also don't question
end

function BeforeKill(killer,victim)
	return true
end

function OnGameEnd()
	
end

function OnChat(message, player)
	local cmd = {}
	for i in string.gmatch(message, "%S+") do
		table.insert(cmd,i)
	end
	if (cmd[1] == "tele") then
		Player_SnapPosTo(tonumber(cmd[2]),tonumber(cmd[3]),player)
	end
	if (cmd[1] == "deadbody") then
		local bodd = Game_StartObjectInit("DeadBody")
		Game_SendObjectToServer(bodd)
	end
end

function OnExile(exiled)
	
end

function OnExileSkip()

end

function CanKill(userinfo,targetinfo)
	return true --the API already prevents the slaying of dead people so make this always return true
end


function DecideImpostors(impostorcount,playerinfos)
	return {playerinfos[1]}
end

