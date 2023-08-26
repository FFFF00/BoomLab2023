-> enter_new_level -> 
-> level1_story ->
-> complete_level ->
->enter_new_level->
-> level2_story ->
->complete_level->
END

=== game_controls ===
= rotate
- 正在旋转
->->
= move
- 正在移动
->->
= move_tile
- 移动图块
->->


=== game_loop ===
+ [rotate] -> game_controls.rotate ->
+ [move] -> game_controls.move ->
+ [move_tile] -> game_controls.move_tile ->
* [exit] -> END
- -> game_loop
    
    
=== enter_new_level ===
呼叫地面控制中心，我们现在进入了一块新的宙域。
到达新宙域，继续执行任务。
地面控制中心，看来这里还不是终点，我们将继续前进。
->->

=== complete_level ===
呼叫地面控制中心，正在离开该宙域。
地面控制中心，我已脱离这片宙域，探索前方边界。
到达目标点，我们真是离地球越来越远了不是吗。
->->
    
=== level1_story === 
探索者号呼叫地面控制中心。
我们已经到达目标宙域，受电磁波影响无法确认周围环境。请下达指示。
控制中心回复：了解，我们已将扫描后的地图发送至操作台。
切换为手动飞行模式，根据地图移动抵达目标位置。
明白，切换为WASD手动飞行模式，前往目标点。
鼠标点击拖动地图，右键旋转。
* [完成关卡 #level-finish]
    探索者号呼叫地面控制中心。
    我们已经到达目标点位置，这看起来像是一个空间裂缝，请下达指示。
    控制中心回复：探索者号，进入裂缝，数据显示它通往新的宙域。
    我有一个疑问：它安全吗？
    控制中心回复：安全，请进入裂缝，它会带领我们前往宇宙边界。
->->    


=== level2_story ===
关卡2
* [complete #level_finish] 
 level finished

->->
        