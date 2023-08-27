->level1_flow ->
->level2_flow ->
END

=== level1_flow() ===
-> level_flow(->empty, ->level1_story, ->level1_end, ->game_loop)
->->

=== level2_flow()===
->level_flow(->enter_new_level, ->level2_story, ->level2_end, ->game_loop)
->->

=== level_flow (-> level_start, ->levelbody, ->level_end, ->loop) ===
-> level_start ->
-> levelbody ->
-> loop -> 
-> level_end ->
->->

=== game_controls ===
= rotate
{shuffle:
    - 观测到剧烈震动，请检查异常！
    - 空间正在转移，重复一遍，空间正在转移。
    - 看起来，有一片空间被移动了。
    - 太奇妙了……请记录下刚才的数据。
    }
->->

= move
{shuffle: 
    - 地面控制中心，我们正在移动。
    - 注意行驶方向，前进。
    }
->->

= move_tile
{shuffle: 
    - nihaoma
    - nihao
    - wohenhao
    - nizhenhao
    }
->->

=== game_loop ===
+ [rotate #rotate]-> game_controls.rotate ->
+ [move #move]-> game_controls.move ->
+ [move_tile #move-tile]-> game_controls.move_tile ->
+ [exit #exit-level] ->->
- -> game_loop
    
    
=== enter_new_level ===
<>{shuffle:
    - nihao
    - nihaoma
    - wohenhao
    }
->->

=== complete_level ===
<>{shuffle:
    - 呼叫地面控制中心，正在离开该宙域。
    - 地面控制中心，我已脱离这片宙域，探索前方边界。
    - 到达目标点，我们真是离地球越来越远了不是吗。
    }
+ [下一关 #next-level] ->->
    
=== empty ===
->->

=== level1_story === 
探索者号呼叫地面控制中心。
我们已经到达目标宙域，受电磁波影响无法确认周围环境。请下达指示。
控制中心回复：了解，我们已将扫描后的地图发送至操作台。
切换为手动飞行模式，根据地图移动抵达目标位置。
明白，切换为WASD手动飞行模式，前往目标点。
鼠标点击拖动地图，右键旋转。
->->    

=== level1_end ===
探索者号呼叫地面控制中心。
我们已经到达目标点位置，这看起来像是一个空间裂缝，请下达指示。
控制中心回复：探索者号，进入裂缝，数据显示它通往新的宙域。
我有一个疑问：它安全吗？
控制中心回复：安全，请进入裂缝，它会带领我们前往宇宙边界。
-> complete_level


=== level2_story ===
关卡2
->->

=== level2_end ===
还是找不到出口，怎么办！
->complete_level
->->
        