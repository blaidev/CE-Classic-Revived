--This is a recreation of the Among Us Classic Gamemode.

local invent_timer = 15
local timedif = 0


function InitializeGamemode()
	Game_CreateRole("Sheriff",{255,216,0},"Find and Kill the [FF1919FF]Impostor[].",{0},1,0,false,true)
	Game_CreateRole("Joker",{129,41,139},"Trick the crewmates into thinking \n\r you are the [FF1919FF]Impostor[].",{},2,1,false,false)
	Game_CreateRole("Hawk-Eyed",{120, 86, 60},"Use your increased sight\n\r to find the [FF1919FF]Impostor[].",{},1,0,true,true)
	Game_CreateRole("Shielded",{107, 107, 107},"Be able to avoid being killed one time.",{},1,0,false,true)
	Game_CreateRole("Witch",{170, 102, 173},"Poison everyone, including [FF1919FF]Impostors[].",{0},2,0,true,false)
	--roles you aren't supposed to see
	Game_CreateRole("Broken",{107, 107, 107},"you shouldn't see this lol",{},1,1,false,true) --impostors can see people with broken shields
	Game_CreateRole("Poisoned",{255, 255, 255},"how",{},1,0,false,true)
	--counts
	Settings_CreateByte("Sheriff Count",0,2,0) -- 0
	Settings_CreateByte("Joker Count",0,3,0) -- 1
	Settings_CreateByte("Hawk-Eyed Count",0,1,0) -- 2
	Settings_CreateByte("Shielded Count",0,2,0) -- 3
	Settings_CreateByte("Witch Count",0,3,0) -- 4
	return {"Roles",2}
end




local function GetRoleAmount(id)
	local pl = Game_GetAllPlayers()
	local idamount = 0
	for i=1, #pl do
		if (pl[i].role == id and not pl[i].IsDead) then
		idamount = idamount + 1
		end
	end
	return idamount
end

function OnTaskCompletionClient(totaltasks,completedtasks,player) --use this to cancel tasks
	if (not player.IsImpostor) then
		return true
	end
	return false
end

function OnTaskCompletionHost(totaltasks,completedtasks,player) --this is ran on the hosts end, usually used to trigger host only events like Winning, we don't need to do anything with this so we're going to make it return false
	return false --this isn't used
end



function OnGameStart()
	invent_timer = 15
	timedif = 0
end



function ShouldSeeRole(rolename,playerinfo)
	return false
end


function OnClientUpdate(timer,timesincelastround)
	if (Client_GetLocalPlayer().luavalue1 == 255) then
		invent_timer = invent_timer - (timer - timedif)
		if (not (math.ceil(timer) == math.ceil(timedif)) and math.ceil(invent_timer) == 5) then
			Client_ShowMessage("You don't feel so well...")
		end
		if (invent_timer < 0) then
			Game_KillPlayer(Client_GetLocalPlayer(),false)
		end
	else
		invent_timer = 15
	end
	timedif = timer
end

function OnPlayerDC(playerinfo)
	if (playerinfo.role == Game_GetRoleIDFromUUID("roles_Sheriff") and Net_AmHost()) then
		local players = Game_GetAllPlayers() --They need to be alive and they can't be an impostor
		for i=#players,1,-1 do
			if (players[i].PlayerName == playerinfo.PlayerName or players[i].IsDead or players[i].IsImpostor or (players[i].role > 0)) then
				table.remove(players,i)
			end
		end
		if (#players == 0) then
			return
		end
		local selected = {players[math.random(1,#players)]}
		Game_SetRoles(selected,{"roles_Sheriff"})
	end
end

function GiveTasks(playerinfo) --Whether or not to assign tasks to a player, this function is a placeholder for proper task assignment control
	return true
end

function OnExile(exiled)
	if (exiled.role == Game_GetRoleIDFromUUID("roles_Sheriff") and Net_AmHost()) then
		local players = Game_GetAllPlayers() --They need to be alive and they can't be an impostor
		for i=#players,1,-1 do
			if (players[i].PlayerName == exiled.PlayerName or players[i].IsDead or players[i].IsImpostor or (players[i].role > 0)) then
				table.remove(players,i)
			end
		end
		if (#players == 0) then
			return
		end
		local selected = {players[math.random(1,#players)],exiled}
		Game_SetRoles(selected,{"roles_Sheriff","None"})
	end
	
	if (exiled.role == Game_GetRoleIDFromUUID("roles_Joker")) then
		if (Net_AmHost) then
			Game_ActivateCustomWin({exiled},"joker_win")
		end
	end
end


function CheckWinCondition(impostors,crewmates,sab,taskscomplete) --required
	if (not sab) then --If the check isn't due to a sabotage
		if (GetRoleAmount(Game_GetRoleIDFromUUID("roles_Witch")) == 0) then
			if (#impostors >= #crewmates) then --crewmates can't win
				return "impostors"
			end
			if (#impostors <= 0) then --no more impostors, crewmates win
				return "crewmates"
			end
		else
			if (#crewmates == 1 and #impostors == 0) then
				return {{crewmates[1]},"witch_win"}
			end
		end
		if (taskscomplete) then --task win
			return "crewmates"
		end
		return "none" --no win conditions have been met.
	else
		return "impostors" --sab loose
	end
end


function CanKill(userinfo,targetinfo)
	if ((userinfo.IsImpostor and not targetinfo.IsImpostor)) then
		if (not targetinfo.role == Game_GetRoleIDFromUUID("roles_Joker")) then
			if (not targetinfo.luavalue1 == 255) then
				return true
			else
				return false
			end
		else
			return false
		end
	end
	if (userinfo.role == Game_GetRoleIDFromUUID("roles_Sheriff")) then
		if (targetinfo.luavalue1 == 255) then
			return false
		else
			return true
		end
	end
	if (userinfo.role == Game_GetRoleIDFromUUID("roles_Witch")) then
		if (targetinfo.luavalue1 == 255) then
			return false
		else
			return true
		end
	end
	return false
end

function OnDeath(victim)
	if (victim.role == Game_GetRoleIDFromUUID("roles_Sheriff") and Net_AmHost()) then
		local players = Game_GetAllPlayers() --They need to be alive and they can't be an impostor
		for i=#players,1,-1 do
			Debug_Log("role:" .. players[i].role)
			if ((players[i].PlayerName == victim.PlayerName or players[i].IsDead or players[i].IsImpostor) or players[i].role > 0) then
				Debug_Log("yeetus!")
				table.remove(players,i)
			end
		end
		if (#players == 0) then
			return
		end
		local selected = {players[math.random(1,#players)],victim}
		Game_SetRoles(selected,{"roles_Sheriff","None"})
	end
end


function BeforeKill(killer,victim)
	if (victim.role == Game_GetRoleIDFromUUID("roles_Shielded")) then
		Game_KillPlayer(killer,false)
		Game_SetRoles({victim},{"roles_Broken"})
		return false
	end
	if (killer.role == Game_GetRoleIDFromUUID("roles_Witch")) then
		victim.luavalue1 = 255
		Game_UpdatePlayerInfo(victim)
		return false
	end
	if (killer.role == Game_GetRoleIDFromUUID("roles_Sheriff")) then
		local players = Game_GetAllPlayers() --They need to be alive and they can't be an impostor
		for i=#players,1,-1 do
			if (players[i].PlayerName == victim.PlayerName or players[i].IsDead or players[i].IsImpostor or players[i].PlayerName == killer.PlayerName or (players[i].role > 0)) then
				table.remove(players,i)
			end
		end
		if (#players == 0) then
			return true
		end
		local selected = {players[math.random(1,#players)],killer}
		Game_SetRoles(selected,{"roles_Sheriff","None"})
	end
	return true
end

function DecideRolesFunction(playerinfos)
	local RolesToGive = {}
	for i=1, Settings_GetByte(0) do
		table.insert(RolesToGive,"roles_Sheriff")
	end
	for i=1, Settings_GetByte(1) do
		table.insert(RolesToGive,"roles_Joker")
	end
	for i=1, Settings_GetByte(2) do
		table.insert(RolesToGive,"roles_Hawk-Eyed")
	end
	for i=1, Settings_GetByte(3) do
		table.insert(RolesToGive,"roles_Shielded")
	end
	for i=1, Settings_GetByte(4) do
		table.insert(RolesToGive,"roles_Witch")
	end
	local Selected = {}
	local SelectedRoles = {}
	for i=1, #RolesToGive do
		local impid = math.random(#playerinfos) --randomly set the impostor id
		table.insert(Selected,playerinfos[impid]) --add it to the selected list
		table.insert(SelectedRoles,RolesToGive[i])
		table.remove(playerinfos,impid) --remove the chosen item from the playerinfo list
	end
	return {Selected,SelectedRoles} -- sets the sheriff's role

end

function DecideRoles(playerinfos)
	return DecideRolesFunction(playerinfos) -- sets the sheriff's role
end


function DecideImpostors(impostorcount,playerinfos)
	local selected = {}
	for i=1, impostorcount do --repeat the below code for how many impostors there are in the settings
		local impid = math.random(#playerinfos) --randomly set the impostor id
		table.insert(selected,playerinfos[impid]) --add it to the selected list
		table.remove(playerinfos,impid) --remove the chosen item from the playerinfo list
	end
	return selected
end
