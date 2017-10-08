#pragma strict

var anim : Animator;


function Start () {
	anim = GetComponent("Animator");
	anim.Play("Wolf_Idle");
}



 
function Update(){
	
	if(Input.GetKey("mouse 0")){
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
     	var hit : RaycastHit;
     	if(Physics.Raycast(ray,hit)){
           
        	if(hit.collider.name == "Run Button"){
           		anim.Play("Wolf_run");
           	}
           	if(hit.collider.name == "Walk Button"){
				anim.Play("Wolf_walk");
           	}
           	if(hit.collider.name == "Idle Button"){
   				anim.Play("Wolf_Idle");        
           	}
           	if(hit.collider.name == "Sniff Button"){
    			anim.Play("Wolf_sniff");       
           	}
           	if(hit.collider.name == "Sniff 2 Button"){
				anim.Play("Wolf_sniffwalk");           
           	}
           	if(hit.collider.name == "Dig Button"){
				anim.Play("Wolf_dig");           
           	}
           	if(hit.collider.name == "Jump Button"){
 				anim.Play("Wolf_jump");          
           	}
           
           
        }
	}
	
}


