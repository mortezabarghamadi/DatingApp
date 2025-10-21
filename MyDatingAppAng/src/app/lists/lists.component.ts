import { Component, OnInit } from '@angular/core';
import { MemberDTO } from '../DTOs/member/MemberDTO';
import { MemberService } from '../services/member.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  members: Partial<MemberDTO[]>;
  predicate = 'liked';

  constructor(private memberService: MemberService) {

  }

  ngOnInit(): void {
    this.loadLikes(this.predicate);
  }

  loadLikes(pre:string) {
    this.predicate=pre;
    this.memberService.getlikes(this.predicate).subscribe(response => {
      this.members = response;
    })
  }

}
