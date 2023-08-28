->level1_flow ->
->level2_flow ->
->level3_flow ->
->level4_flow ->
->level5_flow ->
END

=== level1_flow() ===
-> level_flow(->empty, ->level1_story, ->level1_end, ->game_loop)
->->

=== level2_flow()===
->level_flow(->enter_new_level, ->level_story_empty, ->level_end_empty, ->game_loop)
->->

=== level3_flow ===
-> level_flow(->empty, ->level3_story, ->level_end_empty, ->game_loop )

=== level4_flow ===
-> level_flow(->enter_new_level, ->level_story_empty, ->level_end_empty, ->game_loop)

===level5_flow ===
-> level_flow(->enter_new_level, ->level_story_empty, ->level5_end, ->game_loop )


=== level_flow (-> level_start, ->levelbody, ->level_end, ->loop) ===
-> level_start ->
-> levelbody ->
-> loop -> 
-> level_end ->
->->

=== game_controls ===
= rotate
<>{shuffle:
    - 观测到剧烈震动，请检查异常！
    - 空间正在转移，重复一遍，空间正在转移。
    - 看起来，有一片空间被移动了。
    - 太奇妙了……请记录下刚才的数据。
    }
->->

= move
<>{shuffle: 
    - 地面控制中心，我们正在移动。
    - 注意行驶方向，前进。
    }
->->

= move_tile
<>{shuffle: 
    - 我感到方位正在以一种奇怪的方式改变。
    - 有一片空间正在旋转。
    - 所有人注意，可能会迎来剧烈冲击，抓紧身边的人。
    - 确认到空间变动，小心。
    }
->->

=== game_loop ===
+ [rotate #rotate] -> game_controls.rotate ->
+ [move #move] -> game_controls.move ->
+ [move_tile #move-tile] -> game_controls.move_tile ->
+ [exit #exit-level] ->->
- -> game_loop
    
    
=== enter_new_level ===
{shuffle:
    - 呼叫地面控制中心，我们现在进入了一块新的宙域。
    - 到达新宙域，继续执行任务。
    - 地面控制中心，看来这里还不是终点，我们将继续前进。
    }
->->

=== complete_level ===
{shuffle:
    - 呼叫地面控制中心，正在离开该宙域。
    - 地面控制中心，我已脱离这片宙域，探索前方边界。
    - 到达目标点，我们真是离地球越来越远了不是吗。
    }
+ [下一关 #next-level]->->
    
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


=== level_story_empty ===
->empty
->->

=== level_end_empty ===
->complete_level


=== level3_story ===
呼叫地面控制中心，你们有没有觉得很奇怪？
控制中心回复：从数据上来看，没有奇怪的地方，你的意思是说？
自从靠近宇宙的边界之后，这里的数据到处都显示异常，怪事频发。
但是探索者号的状态却很正常，这种正常就已经很奇怪了。
会不会是有什么东西正在操纵我们？操纵这片空间？
控制中心回复：请将注意力集中在任务上。
->->

=== level5_end ===
……等一下，有点不对劲。
呼叫地面控制中心，我们无法进入空间裂隙，怎么回事？
切换到可见光谱扫描……空间裂隙那边好像有什么东西。
控制中心回复：探索者号，汇报你们所看到的东西。
我不知道，我只看到一块透明平面阻挡了我们的去路。
这块平面的背后有着什么物体正在移动，看不清楚，也扫描不到。
朝头顶上望去，我发现数万个光点排列整齐，在我们身后散发光芒。
感觉像是身处于一个巨大系统的一部分，渺小得能够挤进夹缝。
控制中心回复：探索者号，你们受到了电磁波的干扰，听得见吗？
我们正在尝试穿越屏幕，注意到了一个算法……
控制中心回复：听得见吗？探索者号，听到请回复。
听得见吗？探索者号？
听得见吗？
（已经断开与探索者号的联络。）
->->

        