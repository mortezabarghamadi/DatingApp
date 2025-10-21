import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MemberDTO } from 'src/app/DTOs/member/MemberDTO';
import { MemberService } from 'src/app/services/member.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member: MemberDTO;

  constructor(private memberService: MemberService, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    console.log(this.member);
  }

  addLike(member: MemberDTO) {
    this.memberService.addLike(member.userName).subscribe(() => {
      this.toastr.success("کاربری را لایک کردید.");
    });
  }

}
